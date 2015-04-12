using QuantumShell.Math;
using QuantumShell.QuantumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell
{
    public abstract class IQuantumGate
    {
        public virtual int QubitCount { get; protected set; }
        public virtual IComplexMatrix Transform { get; protected set; }
    }
}
