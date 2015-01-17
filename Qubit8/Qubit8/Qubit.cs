using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

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
            this.Reset();
        }

        public void EntangleWith(Qubit qubit)
        {
            this.EntangledList.Add(qubit);
        }

        public void Reset()
        {
            this.StateVector[0] = Amplitude0;
            this.StateVector[1] = Amplitude1;
            this.EntangledList = new List<Qubit>();
        }

        public string Peek()
        {
            string stateString = this.Amplitude0.ToString() + "|0> + " + this.Amplitude1.ToString() + "|1>";
            return stateString;
        }

        public int Measure()
        {
            double probability0 = Complex.Pow(Amplitude0, 2).Real;
            double probability1 = Complex.Pow(Amplitude1, 2).Real;

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
