using QuantumShell.Math;
using QuantumShell.QuantumModel;

namespace QuantumShell.QuantumGates
{
    class PauliYGate : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }
        public PauliYGate()
        {
            this.QubitCount = 1;
            this.Transform = new ComplexMatrix(2, 2);
            Transform.Matrix[0][0].Real = 0;
            Transform.Matrix[0][1].Imaginary = -1 ;
            Transform.Matrix[1][0].Imaginary = 1;
            Transform.Matrix[1][1].Real = 0;
        }
    }
}
