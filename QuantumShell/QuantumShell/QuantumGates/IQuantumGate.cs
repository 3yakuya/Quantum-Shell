using QuantumShell.QuantumModel;

namespace QuantumShell
{
    public abstract class IQuantumGate
    {
        public virtual int QubitCount { get; protected set; }
        public virtual IComplexMatrix Transform { get; protected set; }
    }
}
