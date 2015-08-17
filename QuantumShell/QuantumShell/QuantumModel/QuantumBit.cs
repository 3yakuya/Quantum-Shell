using System;
using System.Collections.Generic;

namespace QuantumShell.QuantumModel
{
    public interface QuantumBit
    {
        IComplexMatrix StateVector { get; }
        IList<QuantumBit> StateQubitList { get; }
        int QubitIndex { get; }
        IQuantumProvider ComplexProvider { get; }

        void ResetState();
        void JoinState(QuantumBit quantumBit);
        void SetState(IComplexMatrix stateVector);
        void TransformState(IQuantumGate gate);
        void TransformStateControlled(IQuantumGate gate, QuantumBit control);
        void TransformRegisterStateDirected(Func<int, int, int> stateTransform, Func<int, int> indexFunction, QuantumBit control);
        string Peek();
        int Measure();
    }
}
