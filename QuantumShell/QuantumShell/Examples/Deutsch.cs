﻿using QuantumShell.QuantumGates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell.Examples
{
    /// <summary>
    /// This class provides methods to show the possible usage of the Quantum Shell for the Deutsch problem.
    /// The code demonstrates the quantum Deutsch Algorithm.
    /// 
    /// The functions that we check are coded in ConstantFunction and BalancedFunction methods.
    /// 
    /// The measured results informs us if the checked function is constant or balanced. In the first case
    /// the measured value will always be 0. Otherwise it will never be 0 (at least a single 1 in the measured register will occur).
    /// </summary>
    class Deutsch
    {
        public void DeutschQuantumRoutine()
        {
            int registerSize = 2;

            Qubit[] register = InitializeQuantumRegister(registerSize);
            PrepareLowRegister(register);
            PrepareHighRegister(register);

            HadamardGate H = new HadamardGate();

            Console.WriteLine("|-------->Checking if ConstantFunction is balanced or constant...<--------|");
            PeekRegister(register);
            QuantumSubroutine(register, H, ConstantFunction);
            PeekRegister(register);

            MeasureHighRegister(register);
            Console.WriteLine();

            register = ResetQuantumRegister(register);
            Console.WriteLine("|-------->Checking if BalancedFunction is balanced or constant...<--------|");
            PeekRegister(register);
            QuantumSubroutine(register, H, BalancedFunction);
            PeekRegister(register);

            MeasureHighRegister(register);

            Console.ReadLine();
        }

        private void QuantumSubroutine(Qubit[] register, IQuantumGate H, Func<int, int> function)
        {
            Console.WriteLine("\nPerforming the quantum routine...\n");

            register[1].TransformState(H);
            register[0].TransformRegisterStateDirected(Xor, function, register[1]);
            register[1].TransformState(H);
        }

        /// <summary>
        ///This method is used with TransformRegisterStateDirected to build the transformation matrix for Deutsch
        ///quantum routine.
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
        /// It is a constant function.
        /// 
        /// </summary>
        /// <param name="x">Control state index.</param>
        /// <returns>A constant mapping for any x (0 or 1).</returns>
        private int ConstantFunction(int x)
        {
            return 0;
        }

        /// <summary>
        /// This is the function for TransformRegisterStateDirected. It is used to map control index state to other states.
        /// It is a constant function.
        /// 
        /// </summary>
        /// <param name="x">Control state index.</param>
        /// <returns>A balanced mapping for x (0 for half domain and 1 for the other half).</returns>
        private int BalancedFunction(int x)
        {
            return x % 2;
        }

        private Qubit[] InitializeQuantumRegister(int size)
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

            Qubit[] register = new Qubit[size];
            for (int i = 0; i < size; i++)
            {
                register[i] = new Qubit(i);
            }
            return register;
        }

        private void PrepareLowRegister(Qubit[] register)
        {
            register[0].TransformState(new PauliXGate());
            register[0].TransformState(new HadamardGate());
        }

        private void PrepareHighRegister(Qubit[] register)
        {
            
        }

        private void PeekRegister(Qubit[] register)
        {
            Console.WriteLine("Target register state: ");
            Console.WriteLine(register[0].Peek());
            Console.WriteLine("Control register state: ");
            Console.WriteLine(register[1].Peek());
        }

        private void MeasureHighRegister(Qubit[] register)
        {
            Console.WriteLine("\nMeasured result:");
            Console.Write(register[1].Measure());
            Console.WriteLine();
        }

        private Qubit[] ResetQuantumRegister(Qubit[] register)
        {
            register = InitializeQuantumRegister(register.Length);
            PrepareLowRegister(register);
            PrepareHighRegister(register);
            return register;
        }
    }
}
