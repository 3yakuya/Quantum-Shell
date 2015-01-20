using Qubit8.Math;
using Qubit8.QuantumGates;
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

            Qubit qubit = new Qubit(0);
            Qubit qubit2 = new Qubit(1);
            Qubit qubit3 = new Qubit(2);

            //qubit.SetState(stateArray);
            qubit2.SetState(stateArray);
            //qubit3.SetState(stateArray);

            //qubit.TransformState(H);
            qubit2.TransformState(H);
            qubit3.TransformState(X);

            qubit.JoinState(qubit2);
            //qubit2.JoinState(qubit3);
            //qubit3.JoinState(qubit2);
            //qubit2.JoinState(qubit3);

            Console.WriteLine(qubit.Peek());
            Console.WriteLine(qubit2.Peek());
            Console.WriteLine(qubit3.Peek());
            Console.WriteLine();

            Console.WriteLine("-----------------------CNOT1----------------------");
            qubit3.TransformStateControlled(X, qubit2);
            Console.WriteLine(qubit.Peek());
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
