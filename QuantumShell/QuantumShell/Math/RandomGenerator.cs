using QuantumShell.QuantumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell.Math
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random randomGenerator;

        public RandomGenerator()
        {
            this.randomGenerator = new Random();
        }

        public double NextDouble()
        {
            return this.randomGenerator.NextDouble();
        }
    }
}
