using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8
{
    class HadamardGate : IQuantumGate
    {
        private Complex[][] HadamardMatrix;
        public HadamardGate()
        {

        }

        public Qubit[] Transform(Qubit[] inputState)
        {
            Qubit[] outputState = new Qubit[inputState.Length];
            return outputState;//TODO
        }
    }
}
