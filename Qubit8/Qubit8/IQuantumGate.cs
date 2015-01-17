using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8
{
    interface IQuantumGate
    {
        Qubit[] Transform(Qubit[] inputState);
    }
}
