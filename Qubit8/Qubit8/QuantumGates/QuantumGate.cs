using QuantumShell.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell
{
    abstract class QuantumGate
    {
        public virtual int QubitCount { get; protected set; }
        public virtual ComplexMatrix Transform { get; protected set; }
    }
}
