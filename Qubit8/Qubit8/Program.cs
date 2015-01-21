using Qubit8.Math;
using Qubit8.QuantumGates;
using Qubit8.QuantumGates.RotationGates;
using Qubit8.Services;
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
            Qubit qubit = new Qubit(0);
            Qubit qubit1 = new Qubit(1);
            Qubit qubit2 = new Qubit(2);
            Qubit qubit3 = new Qubit(3);
            Qubit qubit4 = new Qubit(4);
            Qubit qubit5 = new Qubit(5);

            Console.WriteLine(qubit.Peek());
            Console.WriteLine(qubit1.Peek());
            PauliXGate X = new PauliXGate();
            qubit1.TransformState(X);

            qubit1.JoinState(qubit);
            qubit1.JoinState(qubit2);
            qubit2.JoinState(qubit3);
            qubit3.JoinState(qubit4);
            qubit4.JoinState(qubit5);

            Console.WriteLine(qubit1.Peek());
            qubit.TransformStateControlled(X, qubit1);
            Console.WriteLine(qubit.Peek());
            Console.ReadLine();

            //Interpreter interpreter = new Interpreter();
            //interpreter.Run();
        }
    }
}
