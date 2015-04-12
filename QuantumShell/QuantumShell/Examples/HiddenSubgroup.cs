using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell.Examples
{
    /// <summary>
    /// This class provides methods to show the possible usage of the Quantum Shell for the hidden subgroup problem.
    /// It demonstrates usage of quantum subroutine of Simon's Algorithm.
    /// 
    /// The function that we want to find the hidden string s for is defined in Function.
    /// WARNING: setting the registerSize to be greater than 10 will cause some methods to take a long time.
    /// 
    /// The measured result represents a random number z such that zs = 0 (so for every index i
    /// z[i]*s[i] = 0 (mod 2)). To determine s we need to build a linear equation set of size n (where 2n
    /// is the size of the register).
    /// </summary>
    class HiddenSubgroup
    {
        public void HiddenSubgroupQuantumSubroutine() 
        {
            int registerSize = 8;

            QuantumBit[] register = InitializeQuantumRegister(registerSize);
            PrepareLowRegister(register);
            PrepareHighRegister(register);

            HadamardGate H = new HadamardGate();

            PeekRegister(register);
            QuantumSubroutine(register, H);
            PeekRegister(register);

            MeasureHighRegister(register);

            Console.ReadLine();
        }

        private void QuantumSubroutine(QuantumBit[] register, IQuantumGate H)
        {
            Console.WriteLine("\nPerforming the quantum subroutine...\n");
            int registerSize = register.Length;

            for (int i = register.Length / 2; i < register.Length; i++)
            {
                register[i].TransformState(H);
            }

            register[registerSize / 2 - 1].TransformRegisterStateDirected(Xor, Function, register[registerSize / 2]);

            for (int i = register.Length / 2; i < register.Length; i++)
            {
                register[i].TransformState(H);
            }
        }

        /// <summary>
        ///This method is used with TransformRegisterStateDirected to build the transformation matrix for Hidden Subgroup
        ///quantum subroutine.
        /// </summary>
        /// <param name="x">A target index will be passed here</param>
        /// <param name="y">A control index will be passed here</param>
        /// <returns>The target index XORed with control index.</returns>
        private static int Xor(int x, int y)
        {
            return x ^ y;
        }

        /// <summary>
        /// This is the function for TransformRegisterStateDirected. It is used to map control index state to other states.
        /// It fulfills the following: f(x) = f(y) => x = y OR x = y XOR s, where s is the hidden string.
        /// 
        /// By default this method's hidden string s = 1010 (binary).
        /// </summary>
        /// <param name="x">Control state index.</param>
        /// <returns>Mapped state index.</returns>
        private int Function(int x)
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
                    return 0;
            }
            return 0;
        }

        private QuantumBit[] InitializeQuantumRegister(int size)
        {
            if (size <= 0)
            {
                size = 2;
                Console.WriteLine("Warning: register size must be a positive even integer. It will be set to 2.");
            }
            else if (size % 2 != 0)
            {
                size -= 1;
                Console.WriteLine("Warning: register should be even sized. It will be resized down by one.");
            }

            QuantumBit[] register = new Qubit[size];
            IQuantumProvider provider = new ComplexProvider();
            for (int i = 0; i < size; i++)
            {
                register[i] = new Qubit(i, provider);
            }
            return register;
        }

        private void PrepareLowRegister(QuantumBit[] register)
        {
            for (int i = 1; i < register.Length / 2; i++)
            {
                register[i].JoinState(register[i - 1]);
            }
        }

        private void PrepareHighRegister(QuantumBit[] register)
        {
            for (int i = register.Length / 2 + 1; i < register.Length; i++)
            {
                register[i].JoinState(register[i - 1]);
            }
        }

        private void PeekRegister(QuantumBit[] register)
        {
            Console.WriteLine("Target register state: ");
            Console.WriteLine(register[register.Length / 2 - 1].Peek());
            Console.WriteLine("Control register state: ");
            Console.WriteLine(register[register.Length / 2].Peek());
        }

        private void MeasureHighRegister(QuantumBit[] register)
        {
            Console.WriteLine("\nMeasured result:");

            int highRegisterStart = register.Length / 2;
            for (int i = register.Length - 1; i >= highRegisterStart; i--)
            {
                Console.Write(register[i].Measure());
            }
            Console.WriteLine();
        }
    }
}
