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
        public Complex Amplitude0
        {
            get
            {
                return this.StateVector[0];
            }
        }

        public Complex Amplitude1
        {
            get
            {
                return this.StateVector[1];
            }
        }

        public Complex[] StateVector { get; set; }
        public IList<Qubit> EntangledList { get; private set; }
        private int entangledPosition = 0;

        public Qubit()
        {
            this.StateVector = new Complex[2];
            this.EntangledList = new List<Qubit>();
            this.Reset();
        }

        //TODO: Ensure a new state vector is created.
        public void EntangleWith(Qubit qubit)
        {
            this.EntangledList.Add(qubit);
        }

        //TODO: Set the StateVecor to the new one with correct values (dis-entangle).
        public void Reset()
        {
            this.StateVector[0] = new Complex(1);
            this.StateVector[1] = new Complex(0);

            this.EntangledList.Clear();
        }

        //TODO: Change to show the entangled state rather than a single Qubit.
        public string Peek()
        {
            string stateString = "";
            if (this.Amplitude0.Real > 0)
            {
                if (this.Amplitude0.Imaginary == 0)
                    stateString += this.Amplitude0.Real.ToString();
                else
                    stateString += this.Amplitude0.ToString();

                stateString += "|0>";
            }

            if (this.Amplitude1.Real > 0)
            {
                stateString += " + ";
                if (this.Amplitude1.Imaginary == 0)
                    stateString += this.Amplitude1.Real.ToString();
                else
                    stateString += this.Amplitude1.ToString();

                stateString += "|1>";
            }
            return stateString;
        }

        //TODO: ensure correct probabilities for the remaining states after removing
        //impossible ones.
        public int Measure()
        {
            double probability0 = GetProbabilityOfMeasuringZero();

            Random random = new Random();
            double randomProbability = random.NextDouble();

            int result;
            if (randomProbability > probability0)
                result = 0;
            else
                result = 1;

            ClearImpossibleStates(result);
            return result;
        }

        private bool BitIsSet(int number, int bit)
        {
            return (number & (1 << bit)) != 0;
        }

        private double GetProbabilityOfMeasuringZero()
        {
            double probabilityOfZero = 0;
            for (int stateIndex = 0; stateIndex < StateVector.Length; stateIndex++)
            {
                if (!BitIsSet(stateIndex, entangledPosition))
                    probabilityOfZero += Complex.Power(StateVector[stateIndex], 2).Real;
            }
            return probabilityOfZero;
        }

        private void ClearImpossibleStates(int measuredValue)
        {
            for (int stateIndex = 0; stateIndex < StateVector.Length; stateIndex++)
            {
                if ((BitIsSet(stateIndex, entangledPosition) && measuredValue == 0) ||
                    (!BitIsSet(stateIndex, entangledPosition) && measuredValue == 1))
                    StateVector[stateIndex] = new Complex(0);
            }
        }
    }
}
