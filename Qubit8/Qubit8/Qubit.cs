using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubit8.Math;

namespace Qubit8
{
    class Qubit
    {
        public ComplexMatrix StateVector { get; private set; }
        public IList<Qubit> StateQubitList { get; private set; }
        private int statePosition = 0;

        public Qubit()
        {
            this.Reset();
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
            var currentStateQubitList = new List<Qubit>(this.StateQubitList);
            currentStateQubitList.Add(qubit);
            var newStateQubitList = GetStateQubitList(currentStateQubitList);
            var qubitsToConsiderInState = FilterQubitsInCurrentState(this.StateQubitList, newStateQubitList);
            qubitsToConsiderInState.Add(this);

            this.StateQubitList = newStateQubitList;
            var fullStateVector = GetStateVector(qubitsToConsiderInState);

            foreach (Qubit stateQubit in StateQubitList)
            {
                stateQubit.StateQubitList = newStateQubitList;
                stateQubit.StateVector = fullStateVector;
                stateQubit.SetSelfStatePosition();
            }
        }

        public void SetState(ComplexMatrix stateVector)
        {
            foreach (Qubit qubit in this.StateQubitList)
                qubit.StateVector = stateVector;
        }

        public void TransformState(ComplexMatrix quantumOperator)
        {
            ComplexMatrix newStateVector = this.StateVector.Dot(quantumOperator);
            SetState(newStateVector);
        }

        public string Peek()
        {
            string stateString = "";
            int numberOfStates = StateVector.ColumnCount;
            int qubitsInState = Convert.ToString(StateVector.ColumnCount, 2).Length - 1;
            bool firstStatePassed = false;

            for (int state = 0; state < numberOfStates; state++)
            {
                if (StateVector.Matrix[0][state] != new Complex(0))
                {
                    if (firstStatePassed)
                        stateString += " + ";
                    firstStatePassed = true;
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
            catch (ArgumentException e)
            {
                return -1;
            }
            return result;
        }

        private void Reset()
        {
            this.StateVector = new ComplexMatrix(1, 2);
            this.StateVector.Matrix[0][0] = new Complex(1);
            this.StateVector.Matrix[0][1] = new Complex(0);

            this.StateQubitList = new List<Qubit>();
            this.StateQubitList.Add(this);
            this.SetSelfStatePosition();
        }

        private void SetSelfStatePosition()
        {
            this.statePosition = this.StateQubitList.IndexOf(this);
        }

        private ComplexMatrix GetStateVector(IList<Qubit> qubitsToAddToState)
        {
            ComplexMatrix stateVector = new ComplexMatrix();
            stateVector.Matrix[0][0] = new Complex(1);
            foreach (Qubit qubit in qubitsToAddToState)
            {
                stateVector = stateVector.Tensorize(qubit.StateVector);
            }
            return stateVector;
        }

        private IList<Qubit> FilterQubitsInCurrentState(IList<Qubit> currentQubitList, IList<Qubit> extendedQubitList)
        {
            return extendedQubitList.Except(currentQubitList).ToList();
        }

        private IList<Qubit> GetStateQubitList(IList<Qubit> currentQubitStateList)
        {
            List<Qubit> stateQubitList = new List<Qubit>(currentQubitStateList);
            foreach (Qubit qubit in this.StateQubitList)
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
            for (int stateIndex = 0; stateIndex < StateVector.ColumnCount; stateIndex++)
            {
                if (!BitIsSet(stateIndex, statePosition))
                    probabilityOfZero += Complex.Power(StateVector.Matrix[0][stateIndex], 2).Real;
            }
            return probabilityOfZero;
        }

        private void ClearImpossibleStates(int measuredValue)
        {
            for (int stateIndex = 0; stateIndex < StateVector.ColumnCount; stateIndex++)
            {
                if ((BitIsSet(stateIndex, statePosition) && measuredValue == 0) ||
                    (!BitIsSet(stateIndex, statePosition) && measuredValue == 1))
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
