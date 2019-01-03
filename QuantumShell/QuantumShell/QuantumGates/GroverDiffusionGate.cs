using QuantumShell.Math;
using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.QuantumGates
{
    class GroverDiffusionGate : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }

        public GroverDiffusionGate()
        {
            this.QubitCount = 8;
            int stateSize = power(2, this.QubitCount);
            this.Transform = new ComplexMatrix(stateSize, stateSize);
            
            for (int row = 0; row < stateSize; row++)
            {
                for (int col = 0; col < stateSize; col++)
                {
                    this.Transform.Matrix[row][col].Real = col == row ? (2.0/stateSize) - 1 : (2.0/stateSize);
                }
            }
        }

        private int power(int number, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= number;
            }

            return result;
        }
    }
}
