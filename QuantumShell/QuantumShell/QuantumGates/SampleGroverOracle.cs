
using QuantumShell.Math;
using QuantumShell.QuantumModel;

namespace QuantumShell.QuantumGates
{
    class SampleGroverOracle : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }

        public SampleGroverOracle()
        {
            this.QubitCount = 3;
            int stateSize = 2**3;
            this.Transform = new ComplexMatrix(stateSize, stateSize);
            for (int row = 0; row < stateSize; row++)
            {
                for (int col = 0; col < stateSize; col++)
                {
                    if (row == col)
                    {
                        Transfrom.Matrix[row][col].Real = SampleOracleFunction(2**row) == 0 ? -1 : 1;
                    }
                    else
                    {
                        Transfrom.Matrix[row][col].Real = 0
                    }
                }
            }
        }

        private int SampleOracleFunction(int argument)
        {
            return argument**3/2;
        }
    }
}
