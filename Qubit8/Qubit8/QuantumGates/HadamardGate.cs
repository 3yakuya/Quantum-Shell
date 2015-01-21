using QuantumShell.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell
{
    class HadamardGate : QuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override ComplexMatrix Transform { get; protected set; }

        public HadamardGate()
        {
            this.QubitCount = 1;
            this.Transform = new ComplexMatrix(2, 2);
            Transform.Matrix[0][0].Real = 1 / System.Math.Sqrt(2);
            Transform.Matrix[0][1].Real = 1 / System.Math.Sqrt(2);
            Transform.Matrix[1][0].Real = 1 / System.Math.Sqrt(2);
            Transform.Matrix[1][1].Real = -1 / System.Math.Sqrt(2);
        }
    }
}
