using QuantumShell.QuantumModel;
using System;

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
