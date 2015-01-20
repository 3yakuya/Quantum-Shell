using Qubit8.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8.QuantumGates
{
    class CNOTGate : QuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override ComplexMatrix Transform { get; protected set; }
        public CNOTGate()
        {
            this.QubitCount = 2;
            this.Transform = new ComplexMatrix(4, 4);
            Transform.Matrix[0][0].Real = 1;
            Transform.Matrix[1][1].Real = 1;
            Transform.Matrix[2][3].Real = 1;
            Transform.Matrix[3][2].Real = 1;
        }
    }
}
