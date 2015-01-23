using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumGates.RotationGates;
using QuantumShell.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Qubit[] register = new Qubit[8];
            for (int i = 0; i < 8; i++)
                register[i] = new Qubit(i);

            register[4].TransformState(new PauliXGate());
            register[5].TransformState(new PauliXGate());
            register[6].TransformState(new PauliXGate());
            register[7].TransformState(new PauliXGate());


            register[0].JoinState(register[1]);
            register[1].JoinState(register[2]);
            register[2].JoinState(register[3]);

            register[4].JoinState(register[5]);
            register[5].JoinState(register[6]);
            register[6].JoinState(register[7]);

            Console.WriteLine(register[0].Peek());
            Console.WriteLine(register[4].Peek());


            register[3].TransformMultiStateControlled(Xor, f, register[4]);

            Console.WriteLine(register[0].Peek());
            Console.ReadLine();

            //Interpreter interpreter = new Interpreter();
            //interpreter.Run();
        }

        private static int Xor(int x, int y)
        {
            return x % y;
        }

        private static int f(int x)
        {
            return x-5;
        }
    }
}
