using QuantumShell.Math;
using QuantumShell.QuantumModel;

namespace QuantumShell.QuantumGates
{
    class GroverDiffusionGate : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }

        public GroverDiffusionGate()
        {
            this.QubitCount = 3;
            int stateSize = 2**3;
            this.Transform = new ComplexMatrix(stateSize, stateSize);
            
            for (int row = 0; row < stateSize; row++)
            {
                for (int col = 0; col < stateSize; col++)
                {
                    Transfrom.Matrix[row][col].Real = col == row ? (2/stateSize) - 1 : (2/stateSize);
                }
            }
        }
    }
}
