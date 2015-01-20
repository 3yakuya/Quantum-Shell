using Qubit8.Math;
using Qubit8.QuantumGates;
using Qubit8.QuantumGates.RotationGates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexMatrix stateArray = new ComplexMatrix(1, 2);
            stateArray.Matrix[0][0].Real = 0;
            stateArray.Matrix[0][1].Real = 1;

            HadamardGate H = new HadamardGate();
            PauliXGate X = new PauliXGate();
            PauliIGate I = new PauliIGate();
            PauliZGate Z = new PauliZGate();
            PauliYGate Y = new PauliYGate();
            Pi8Gate T = new Pi8Gate();
            R2ReversedGate R2 = new R2ReversedGate();
            R3ReversedGate R3 = new R3ReversedGate();
            R4ReversedGate R4 = new R4ReversedGate();

            Qubit qubit = new Qubit(0);
            Qubit qubit1 = new Qubit(1);
            qubit1.TransformState(X);
            Qubit qubit2 = new Qubit(2);
            Qubit qubit3 = new Qubit(3);
            qubit3.TransformState(X);

            qubit2.TransformState(H);

            Console.WriteLine(qubit.Peek());

            qubit.JoinState(qubit1);
            qubit2.JoinState(qubit3);
            qubit1.JoinState(qubit2);

            Console.WriteLine(qubit.Peek());

            qubit3.TransformStateControlled(X, qubit2);

            Console.WriteLine(qubit.Peek());


            Console.ReadLine();
        }
    }
}
