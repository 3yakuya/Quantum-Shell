using QuantumShell.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell.QuantumGates
{
    class Pi8Gate : QuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override ComplexMatrix Transform { get; protected set; }
        public Pi8Gate()
        {
            this.QubitCount = 1;
            this.Transform = new ComplexMatrix(2, 2);
            Transform.Matrix[0][0].Real = 1;
            Transform.Matrix[0][1].Real = 0;
            Transform.Matrix[1][0].Real = 0;
            Transform.Matrix[1][1].Real = 1 / System.Math.Sqrt(2);
            Transform.Matrix[1][1].Imaginary = 1 / System.Math.Sqrt(2);
        }
    }
}
