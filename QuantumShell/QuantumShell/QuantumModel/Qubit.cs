using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantumShell.Math;

namespace QuantumShell
{
    class Qubit
    {
        public ComplexMatrix StateVector { get; private set; }
        public IList<Qubit> StateQubitList { get; private set; }
        public int QubitIndex { get; private set; }

        public Qubit(int index)
        {
            this.QubitIndex = index;
            this.Reset();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + this.QubitIndex;
        }

        public void ResetState()
        {
            foreach (Qubit qubit in this.StateQubitList)
                qubit.Reset();
        }

        public void JoinState(Qubit qubit)
        {
            if (this.StateQubitList.Contains(qubit))
                return;

            if (System.Math.Abs(this.QubitIndex - qubit.QubitIndex) != 1)
                throw new ArgumentException("Only a consistent state may be formed.");

            var currentStateQubitList = new List<Qubit>(this.StateQubitList);
            currentStateQubitList.Add(qubit);

            var qubitsToConsiderInState = new List<Qubit>();
            qubitsToConsiderInState.Add(this);
            qubitsToConsiderInState.Add(qubit);
            qubitsToConsiderInState = qubitsToConsiderInState.OrderBy(q => q.QubitIndex).ToList();

            this.StateQubitList = GetStateQubitList(currentStateQubitList);
            var fullStateVector = GetStateVector(qubitsToConsiderInState);

            foreach (Qubit stateQubit in StateQubitList)
            {
                stateQubit.StateQubitList = this.StateQubitList;
                stateQubit.StateVector = fullStateVector;
                stateQubit.SetSelfStateIndex();
            }
        }

        public void SetState(ComplexMatrix stateVector)
        {
            foreach (Qubit qubit in this.StateQubitList)
                qubit.StateVector = stateVector;
        }

        public void TransformState(QuantumGate gate)
        {
            ComplexMatrix quantumOperator = BuildStateQuantumOperator(gate);
            ComplexMatrix newStateVector = this.StateVector.Dot(quantumOperator);
            SetState(newStateVector);
        }

        public void TransformStateControlled(QuantumGate gate, Qubit controlQubit)
        {
            if (controlQubit == this)
                throw new ArgumentException("Target qubit can not be used to control itself.");
            JoinState(controlQubit);
            ComplexMatrix controlledOperator = BuildControlledQuantumOperator(gate, controlQubit, this);
            ComplexMatrix newStateVector = this.StateVector.Dot(controlledOperator);
            SetState(newStateVector);
        }

        public void TransformMultiStateDirected(Func<int, int, int> stateTransform, Func<int, int> f, Qubit controlRepresentant)
        {
            if (controlRepresentant.StateVector == this.StateVector)
                throw new ArgumentException("Target and control registers must be separate.");
            if (controlRepresentant.QubitIndex < this.QubitIndex)
                throw new ArgumentException("Control register must be higher than target register.");

            JoinState(controlRepresentant);
            ComplexMatrix fullStateOperator = BuildDirectedTransform(stateTransform, f, controlRepresentant);
            ComplexMatrix newStateVector = this.StateVector.Dot(fullStateOperator);
            SetState(newStateVector);
            //NormalizeStateVector();
        }


        public string Peek()
        {
            string stateString = "";
            int numberOfStates = StateVector.ColumnCount;
            int qubitsInState = Convert.ToString(StateVector.ColumnCount, 2).Length - 1;
            bool firstStatePeeked = false;

            for (int state = 0; state < numberOfStates; state++)
            {
                if (StateVector.Matrix[0][state] != new Complex(0))
                {
                    if (firstStatePeeked)
                        stateString += " + ";
                    firstStatePeeked = true;
                    stateString += StateVector.Matrix[0][state];
                    stateString += "|" + Convert.ToString(state, 2).PadLeft(qubitsInState, '0') + ">";
                }
            }
            return stateString;
        }

        public int Measure()
        {
            double probability0 = GetProbabilityOfMeasuringZero();

            Random random = new Random();
            double randomProbability = random.NextDouble();

            int result;
            if (randomProbability < probability0)
                result = 0;
            else
                result = 1;

            ClearImpossibleStates(result);
            try
            {
                NormalizeStateVector();
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return result;
        }

        private ComplexMatrix BuildDirectedTransform(Func<int, int, int> stateTransform, Func<int, int> f, Qubit controlRepresentant)
        {
            int stateSize = StateVector.Matrix[0].Count;
            ComplexMatrix directedMultiQubitTransform = new ComplexMatrix(stateSize, stateSize);
            //Complex amplitude = GetEqualWeightAmplitude(GetPossibleStatesCount(controlRepresentant.StateVector));
            Complex amplitude = new Complex(1);

            for (int stateColumn = 0; stateColumn < stateSize; stateColumn++)
            {
                int controlStateIndex = GetHigherRegisterState(stateColumn, StateQubitList.Count);
                int targetStateIndex = GetLowerRegisterState(stateColumn, StateQubitList.Count);

                int processedControlParameter = f(controlStateIndex);
                targetStateIndex = stateTransform(targetStateIndex, processedControlParameter);
                if (targetStateIndex < 0 || targetStateIndex > (int) System.Math.Pow(2, StateQubitList.Count))
                    throw new ArgumentException("Function processing the directed qubit returns incorrect values.");

                int registerStateIndex = GetCompleteRegisterState(controlStateIndex, targetStateIndex, StateQubitList.Count);
                directedMultiQubitTransform.Matrix[stateColumn][registerStateIndex] = amplitude;
            }
            return directedMultiQubitTransform;
        }

        private int GetCompleteRegisterState(int higherRegisterState, int lowerRegisterState, int qubitInStateCount)
        {
            int completeRegisterState = higherRegisterState << (qubitInStateCount / 2);
            completeRegisterState += lowerRegisterState;
            return completeRegisterState;
        }

        private int GetHigherRegisterState(int joinedStateIndex, int qubitsInStateCount)
        {
            return joinedStateIndex >> (qubitsInStateCount / 2);
        }

        private int GetLowerRegisterState(int joinedStateIndex, int qubitsInStateCount)
        {
            return joinedStateIndex % (int) System.Math.Pow(2, qubitsInStateCount / 2);
        }

        private IList<int> GetStatesAllPossibleFunctionValues(Func<int, int> f, ComplexMatrix stateVector)
        {
            List<int> possibleFunctionValues = new List<int>();
            for (int stateIndex = 0; stateIndex < stateVector.Matrix[0].Count; stateIndex++)
            {
                Complex amplitude = stateVector.Matrix[0][stateIndex];
                if (amplitude.Real != 0 || amplitude.Imaginary != 0)
                {
                    possibleFunctionValues.Add(f(stateIndex));
                }
            }
            return possibleFunctionValues;
        }

        private Complex GetEqualWeightAmplitude(int possibleStatesCount)
        {
            return new Complex(1 / System.Math.Sqrt(possibleStatesCount));
        }

        private int GetPossibleStatesCount(ComplexMatrix stateVector) 
        {
            int possibleStatesCount = 0;
            foreach (Complex amplitude in stateVector.Matrix[0])
            {
                if (amplitude.Real != 0 || amplitude.Imaginary != 0)
                    possibleStatesCount++;
            }
            return possibleStatesCount;
        }

        private ComplexMatrix BuildControlledQuantumOperator(QuantumGate gate, Qubit control, Qubit target)
        {
            int stateSize = StateVector.Matrix[0].Count;
            int numberOfQubits = StateQubitList.Count;
            int controlBit = StateQubitList.IndexOf(control);
            int targetBit = StateQubitList.IndexOf(target);
            ComplexMatrix controlledTransform = new ComplexMatrix().IdentityMatrix(stateSize);

            for (int row = 0; row < stateSize; row++)
            {
                if (BitIsSet(row, controlBit))
                {
                    for (int column = 0; column < stateSize; column++)
                    {
                        bool correctState = true;
                        for (int stateBit = 0; (stateBit < numberOfQubits) && correctState; stateBit++)
                        {
                            if ((BitIsSet(column, stateBit) != BitIsSet(row, stateBit)) && (stateBit != targetBit))
                            {
                                    correctState = false;
                            }
                        }
                        if (correctState)
                        {
                            int transformRow = Convert.ToInt32(BitIsSet(row, targetBit));
                            int transformColumn = Convert.ToInt32(BitIsSet(column, targetBit));
                            controlledTransform.Matrix[row][column] = gate.Transform.Matrix[transformRow][transformColumn]; 
                        }
                    }
                }
            }
            return controlledTransform;
        }

        private ComplexMatrix BuildStateQuantumOperator(QuantumGate gate)
        {
            ComplexMatrix stateOperator = new ComplexMatrix();
            stateOperator.Matrix[0][0].Real = 1;
            ComplexMatrix identity = new ComplexMatrix().IdentityMatrix(2);

            for (int qubitIndex = 0; qubitIndex < this.StateQubitList.Count; qubitIndex++)
            {
                if (qubitIndex == StateQubitList.IndexOf(this))
                {
                    stateOperator = gate.Transform.Tensorize(stateOperator);
                    qubitIndex += gate.QubitCount - 1;
                }
                else
                {
                    stateOperator = identity.Tensorize(stateOperator);
                }
            }
            return stateOperator;
        } 

        private void Reset()
        {
            this.StateVector = new ComplexMatrix(1, 2);
            this.StateVector.Matrix[0][0] = new Complex(1);
            this.StateVector.Matrix[0][1] = new Complex(0);

            this.StateQubitList = new List<Qubit>();
            this.StateQubitList.Add(this);
            this.SetSelfStateIndex();
        }

        private void SetSelfStateIndex()
        {
            this.StateQubitList = StateQubitList.OrderBy(q => q.QubitIndex).ToList();
        }

        private ComplexMatrix GetStateVector(IList<Qubit> qubitsToAddToState)
        {
            ComplexMatrix stateVector = new ComplexMatrix();
            stateVector.Matrix[0][0] = new Complex(1);
            foreach (Qubit qubit in qubitsToAddToState)
            {
                stateVector = qubit.StateVector.Tensorize(stateVector);
            }
            return stateVector;
        }

        private IList<Qubit> GetStateQubitList(IList<Qubit> currentQubitStateList)
        {
            List<Qubit> stateQubitList = new List<Qubit>(currentQubitStateList);
            foreach (Qubit qubit in currentQubitStateList)
            {
                stateQubitList = stateQubitList.Union(qubit.StateQubitList).ToList();
            }
            return stateQubitList;
        }

        private bool BitIsSet(int number, int bit)
        {
            return (number & (1 << bit)) != 0;
        }

        private double GetProbabilityOfMeasuringZero()
        {
            double probabilityOfZero = 0;
            int qubitIndex = StateQubitList.IndexOf(this);
            for (int stateIndex = 0; stateIndex < StateVector.ColumnCount; stateIndex++)
            {
                if (!BitIsSet(stateIndex, qubitIndex))
                    probabilityOfZero += Complex.Power(StateVector.Matrix[0][stateIndex], 2).Real;
            }
            return probabilityOfZero;
        }

        private void ClearImpossibleStates(int measuredValue)
        {
            int qubitIndex = StateQubitList.IndexOf(this);
            for (int stateIndex = 0; stateIndex < StateVector.ColumnCount; stateIndex++)
            {
                if ((BitIsSet(stateIndex, qubitIndex) && measuredValue == 0) ||
                    (!BitIsSet(stateIndex, qubitIndex) && measuredValue == 1))
                    StateVector.Matrix[0][stateIndex] = new Complex(0);
            }
        }

        private void NormalizeStateVector()
        {
            double remainingProbabilitiesSum = 0;
            foreach (Complex amplitude in this.StateVector.Matrix[0])
            {
                remainingProbabilitiesSum += Complex.Power(amplitude, 2).Real;
            }

            Complex normalizer = new Complex(System.Math.Sqrt(remainingProbabilitiesSum));
            for (int amplitudeIndex = 0; amplitudeIndex < StateVector.ColumnCount; amplitudeIndex++)
            {
                StateVector.Matrix[0][amplitudeIndex] /= normalizer;
            }
        }
    }
}
