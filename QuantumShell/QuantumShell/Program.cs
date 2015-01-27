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

            register[0].JoinState(register[1]);
            register[1].JoinState(register[2]);
            register[2].JoinState(register[3]);

            register[0].TransformState(new PauliXGate());

            register[4].JoinState(register[5]);
            register[5].JoinState(register[6]);
            register[6].JoinState(register[7]);
            register[4].TransformState(new HadamardGate());
            register[5].TransformState(new HadamardGate());
            register[6].TransformState(new HadamardGate());
            register[7].TransformState(new HadamardGate());

            register[3].TransformRegisterStateDirected(Xor, f, register[4]);

            register[4].TransformState(new HadamardGate());
            register[5].TransformState(new HadamardGate());
            register[6].TransformState(new HadamardGate());
            register[7].TransformState(new HadamardGate());

            Console.WriteLine(register[4].Peek() + "\n");

            Console.WriteLine(register[4].Measure());
            Console.WriteLine(register[5].Measure());
            Console.WriteLine(register[6].Measure());
            Console.WriteLine(register[7].Measure());

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
            switch (x)
            {
                case 0:
                    return 14;
                case 1:
                    return 9;
                case 2:
                    return 8;
                case 3:
                    return 2;
                case 4:
                    return 12;
                case 5:
                    return 0;
                case 6:
                    return 3;
                case 7:
                    return 6;
                case 8:
                    return 8;
                case 9:
                    return 2;
                case 10:
                    return 14;
                case 11:
                    return 9;
                case 12:
                    return 3;
                case 13:
                    return 6;
                case 14:
                    return 12;
                case 15:
                    return 6;
            }
            return 0;
        }
    }
}
