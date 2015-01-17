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

        public Qubit()
        {
            this.StateVector = new Complex[2];
            this.EntangledList = new List<Qubit>();
            this.Reset();
        }

        public void EntangleWith(Qubit qubit)
        {
            this.EntangledList.Add(qubit);
        }

        public void Reset()
        {
            this.StateVector[0] = new Complex(1);
            this.StateVector[1] = new Complex(0);
            this.EntangledList.Clear();
        }

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

        public int Measure()
        {
            double probability0 = Complex.Power(Amplitude0, 2).Real;
            double probability1 = Complex.Power(Amplitude1, 2).Real;

            Random random = new Random();
            double randomProbability = random.NextDouble();
            if (probability0 > probability1)
            {
                if (randomProbability < probability0)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (randomProbability < probability1)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
