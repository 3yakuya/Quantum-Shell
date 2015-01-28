using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumGates.RotationGates;
using QuantumShell.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            register[4].JoinState(register[5]);
            register[5].JoinState(register[6]);
            register[6].JoinState(register[7]);

            QuantumGate QFT = new QuantumFourierTransform(4);
            QuantumGate IQFT = new InverseQuantumFourierTransform(4);

            register[0].TransformState(new PauliXGate());

            register[4].TransformState(QFT);
            register[3].TransformRegisterStateDirected(factorFunc, retx, register[4]);
            register[4].TransformState(IQFT);
            Console.WriteLine(register[4].Peek());

            double sum = 0;
            foreach (var prob in register[0].StateVector.Matrix[0])
                sum += QuantumShell.Math.Complex.Power(prob, 2).Real;
            Console.WriteLine("Total probability: " + sum);

            Console.WriteLine(register[4].Measure());
            Console.WriteLine(register[5].Measure());
            Console.WriteLine(register[6].Measure());
            Console.WriteLine(register[7].Measure());
            Console.ReadLine();

            //Interpreter interpreter = new Interpreter();
            //interpreter.Run();
        }

        private static int factorFunc(int x, int y)
        {
            if (x < 15)
            {
                int result = x;
                for (int i = 0; i < y; i++)
                {
                    result = (result * 11) % 15;
                }
                return result;
            }
            else
                return x;
        }

        private static int retx(int x)
        {
            return x;
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
