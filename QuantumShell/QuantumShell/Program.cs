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

            register[2].TransformState(new HadamardGate());
            register[3].TransformState(new HadamardGate());



            register[0].JoinState(register[1]);

            register[2].JoinState(register[3]);

            Console.WriteLine(register[0].Peek());
            Console.WriteLine(register[2].Peek());
            Console.WriteLine();


            register[1].TransformMultiStateDirected(Xor, f, register[2]);

            Console.WriteLine(register[0].Peek());
            Console.ReadLine();

            //Interpreter interpreter = new Interpreter();
            //interpreter.Run();
        }

        private static int Xor(int x, int y)
        {
            return x ^ y;
        }

        private static int f(int x)
        {
            return x;
        }
    }
}
