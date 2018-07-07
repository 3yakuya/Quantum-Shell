using System;
using System.Collections.Generic;
using System.Linq;
using QuantumShell.QuantumModel;

namespace QuantumShell
{
    class Qubit : QuantumBit
    {
        public IComplexMatrix StateVector { get; private set; }
        public IList<QuantumBit> StateQubitList { get; private set; }
        public int QubitIndex { get; private set; }
        public IQuantumProvider ComplexProvider { get; private set; }

        public Qubit(int index, IQuantumProvider complexProvider)
        {
            this.ComplexProvider = complexProvider;
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

        public void JoinState(QuantumBit qubit)
        {
            if (this.StateQubitList.Contains(qubit))
                return;

            if (System.Math.Abs(this.QubitIndex - qubit.QubitIndex) != 1)
                throw new ArgumentException("Only a consistent state may be formed.");

            var currentStateQubitList = ComplexProvider.List(this.StateQubitList);
            currentStateQubitList.Add(qubit);

            var qubitsToConsiderInState = ComplexProvider.List();
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

        public void SetState(IComplexMatrix stateVector)
        {
            foreach (Qubit qubit in this.StateQubitList)
                qubit.StateVector = stateVector;
        }

        public void TransformState(IQuantumGate gate)
        {
            IComplexMatrix quantumOperator = BuildStateQuantumOperator(gate);
            IComplexMatrix newStateVector = quantumOperator.Dot(this.StateVector);
            SetState(newStateVector);
        }

        public void TransformStateControlled(IQuantumGate gate, QuantumBit controlQubit)
        {
            if (controlQubit == this)
                throw new ArgumentException("Target qubit can not be used to control itself.");
            JoinState(controlQubit);
            IComplexMatrix controlledOperator = BuildControlledQuantumOperator(gate, controlQubit, this);
            IComplexMatrix newStateVector = controlledOperator.Dot(this.StateVector);
            SetState(newStateVector);
        }

        public void TransformRegisterStateDirected(Func<int, int, int> stateTransform, Func<int, int> f, QuantumBit controlRepresentant)
        {
            if (controlRepresentant.StateVector == this.StateVector)
                throw new ArgumentException("Target and control registers must be separate.");
            if (controlRepresentant.QubitIndex < this.QubitIndex)
                throw new ArgumentException("Control register must be higher than target register.");
            if (controlRepresentant.StateQubitList.Count < this.StateQubitList.Count)
                throw new ArgumentException("Control register must contain at least as many qubits as target register.");

            int targetRegisterSize = this.StateQubitList.Count;
            JoinState(controlRepresentant);
            IComplexMatrix fullStateOperator = BuildDirectedTransform(stateTransform, f, targetRegisterSize);
            IComplexMatrix newStateVector = fullStateOperator.Dot(this.StateVector);
            SetState(newStateVector);
        }

        public string Peek()
        {
            string stateString = "";
            int numberOfStates = StateVector.RowCount;
            int qubitsInState = Convert.ToString(StateVector.RowCount, 2).Length - 1;
            bool firstStatePeeked = false;

            for (int state = 0; state < numberOfStates; state++)
            {
                if (!StateVector.Matrix[state][0].EqualTo(ComplexProvider.Complex(0)))
                {
                    if (firstStatePeeked)
                        stateString += " + ";
                    firstStatePeeked = true;
                    stateString += StateVector.Matrix[state][0];
                    stateString += "|" + Convert.ToString(state, 2).PadLeft(qubitsInState, '0') + ">";
                }
            }
            return stateString;
        }

        public int Measure()
        {
            double probability0 = GetProbabilityOfMeasuringZero();

            IRandomGenerator random = ComplexProvider.RandomGenerator();
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

        private IComplexMatrix BuildDirectedTransform(Func<int, int, int> stateTransform, Func<int, int> f, int targetRegisterSize)
        {
            int stateSize = StateVector.RowCount;
            IComplexMatrix directedMultiQubitTransform = ComplexProvider.ComplexMatrix(stateSize, stateSize);
            IComplex amplitude = ComplexProvider.Complex(1);

            for (int stateIndex = 0; stateIndex < stateSize; stateIndex++)
            {
                int controlStateIndex = GetHighRegisterState(stateIndex, targetRegisterSize);
                int targetStateIndex = GetLowRegisterState(stateIndex, targetRegisterSize);

                int processedControlParameter = f(controlStateIndex);
                targetStateIndex = stateTransform(targetStateIndex, processedControlParameter);
                if (targetStateIndex < 0 || targetStateIndex >= (int) System.Math.Pow(2, targetRegisterSize))
                    throw new ArgumentException("Function processing the directed qubit returns incorrect values.");

                int registerStateIndex = GetCompleteRegisterState(controlStateIndex, targetStateIndex, targetRegisterSize);
                directedMultiQubitTransform.Matrix[registerStateIndex][stateIndex]= amplitude;
            }
            return directedMultiQubitTransform;
        }

        private int GetCompleteRegisterState(int highRegisterState, int lowRegisterState, int lowRegisterSize)
        {
            int completeRegisterState = highRegisterState << lowRegisterSize;
            completeRegisterState += lowRegisterState;
            return completeRegisterState;
        }

        private int GetHighRegisterState(int joinedStateIndex, int lowRegisterSize)
        {
            return joinedStateIndex >> lowRegisterSize;
        }

        private int GetLowRegisterState(int joinedStateIndex, int lowRegisterSize)
        {
            return joinedStateIndex % (int) System.Math.Pow(2, lowRegisterSize);
        }

        private IComplexMatrix BuildControlledQuantumOperator(IQuantumGate gate, QuantumBit control, QuantumBit target)
        {
            int stateSize = StateVector.RowCount;
            int numberOfQubits = StateQubitList.Count;
            int controlBit = StateQubitList.IndexOf(control);
            int targetBit = StateQubitList.IndexOf(target);
            IComplexMatrix controlledTransform = ComplexProvider.ComplexMatrix().IdentityMatrix(stateSize);

            for (int column = 0; column < stateSize; column++)
            {
                if (BitIsSet(column, controlBit))
                {
                    for (int row = 0; row < stateSize; row++)
                    {
                        bool correctState = true;
                        for (int stateBit = 0; (stateBit < numberOfQubits) && correctState; stateBit++)
                        {
                            if ((BitIsSet(row, stateBit) != BitIsSet(column, stateBit)) && (stateBit != targetBit))
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

        private IComplexMatrix BuildStateQuantumOperator(IQuantumGate gate)
        {
            IComplexMatrix stateOperator = ComplexProvider.ComplexMatrix();
            stateOperator.Matrix[0][0].Real = 1;
            IComplexMatrix identity = ComplexProvider.ComplexMatrix().IdentityMatrix(2);

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
            this.StateVector = ComplexProvider.ComplexMatrix(2, 1);
            this.StateVector.Matrix[0][0] = ComplexProvider.Complex(1);
            this.StateVector.Matrix[1][0] = ComplexProvider.Complex(0);

            this.StateQubitList = ComplexProvider.List();
            this.StateQubitList.Add(this);
            this.SetSelfStateIndex();
        }

        private void SetSelfStateIndex()
        {
            this.StateQubitList = StateQubitList.OrderBy(q => q.QubitIndex).ToList();
        }

        private IComplexMatrix GetStateVector(IList<QuantumBit> qubitsToAddToState)
        {
            IComplexMatrix stateVector = ComplexProvider.ComplexMatrix();
            stateVector.Matrix[0][0] = ComplexProvider.Complex(1);
            foreach (QuantumBit qubit in qubitsToAddToState)
            {
                stateVector = qubit.StateVector.Tensorize(stateVector);
            }
            return stateVector;
        }

        private IList<QuantumBit> GetStateQubitList(IList<QuantumBit> currentQubitStateList)
        {
            IList<QuantumBit> stateQubitList = ComplexProvider.List(currentQubitStateList);
            foreach (Qubit qubit in currentQubitStateList)
            {
                stateQubitList = stateQubitList.Union(qubit.StateQubitList).ToList();
            }
            return stateQubitList;
        }

        private bool BitIsSet(int number, int bit)
        {
            bool val = (number & (1 << bit)) != 0;
            return val;
        }

        private double GetProbabilityOfMeasuringZero()
        {
            double probabilityOfZero = 0;
            int qubitIndex = StateQubitList.IndexOf(this);
            for (int stateIndex = 0; stateIndex < StateVector.RowCount; stateIndex++)
            {
                if (!BitIsSet(stateIndex, qubitIndex))
                {
                    double absoluteValue = StateVector.Matrix[stateIndex][0].Absolute();
                    probabilityOfZero += absoluteValue * absoluteValue;
                }
            }
            return probabilityOfZero;
        }

        private void ClearImpossibleStates(int measuredValue)
        {
            int qubitIndex = StateQubitList.IndexOf(this);
            for (int stateIndex = 0; stateIndex < StateVector.RowCount; stateIndex++)
            {
                if ((BitIsSet(stateIndex, qubitIndex) && measuredValue == 0) ||
                    (!BitIsSet(stateIndex, qubitIndex) && measuredValue == 1))
                    StateVector.Matrix[stateIndex][0] = ComplexProvider.Complex(0);
            }
        }

        private void NormalizeStateVector()
        {
            double remainingProbabilitiesSum = 0;
            foreach (IList<IComplex> amplitude in this.StateVector.Matrix)
            {
                IComplex toAdd = ComplexProvider.Complex(amplitude[0].Real, amplitude[0].Imaginary);
                double absoluteValue = toAdd.Absolute();
                remainingProbabilitiesSum += absoluteValue * absoluteValue;
            }

            IComplex normalizer = ComplexProvider.Complex(System.Math.Sqrt(remainingProbabilitiesSum));
            for (int amplitudeIndex = 0; amplitudeIndex < StateVector.RowCount; amplitudeIndex++)
            {
                StateVector.Matrix[amplitudeIndex][0].Divide(normalizer);
            }
        }
    }
}
