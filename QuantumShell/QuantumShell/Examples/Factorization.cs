using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.Examples
{
    /// <summary>
    /// This class provides methods to show the possible usage of the Quantum Shell for the factorization problem.
    /// The code demostrates the quantum subroutine for Shor's Algorithm solving Order Finding problem
    /// (one to which the Integer Factorization problem can be reduced).
    /// 
    /// Integer to be factorized and necessary a parameter are in FactorFunction method.
    /// WARNING: setting the registerSize to be greater than 10 will cause some methods to take a long time.
    /// 
    /// The measured result represents a number x such that x/2^n (where 2n is the register size) represents k/r,
    /// where r is the order we are looking for and k is a random ingteger from [0, r-1].
    /// </summary>
    class Factorization
    {
        public void OrderFindingQuantumSubroutine()
        {
            int registerSize = 10;

            QuantumBit[] register = InitializeQuantumRegister(registerSize);
            PrepareLowRegister(register);
            PrepareHighRegister(register);

            IQuantumGate QFT = new QuantumFourierTransform(registerSize / 2);
            IQuantumGate IQFT = new InverseQuantumFourierTransform(registerSize / 2);

            PeekRegister(register);
            QuantumSubroutine(register, QFT, IQFT);
            PeekRegister(register);

            MeasureHighRegister(register);
            
            Console.ReadLine();
        }

        private void QuantumSubroutine(QuantumBit[] register, IQuantumGate QFT, IQuantumGate IQFT)
        {
            Console.WriteLine("\nPerforming the quantum subroutine...\n");
            int registerSize = register.Length;
            register[registerSize / 2].TransformState(QFT);
            register[registerSize / 2 - 1].TransformRegisterStateDirected(FacorFunction, MapIndex, register[registerSize / 2]);
            register[registerSize / 2].TransformState(IQFT);
        }

        /// <summary>
        /// This method is used with TransformRegisterStateDirected to build the transformation matrix for Order Finding
        /// quantum subroutine.
        /// factorizedNumber represents the number to be factorized.
        /// a is a number that should be coprime with factorizedNumber. We are trying to find order of a mod factorizedNumber.
        /// Remember that 2^n must be greater or equal 2*r^2.
        /// 
        /// By default this method is used to find order of 8 (mod 21) (which is 2).
        /// </summary>
        /// <param name="x">A target index will be passed here</param>
        /// <param name="y">A control index will be passed here</param>
        /// <returns>New index for target register (in other words: how it is going to be transformed).</returns>
        private int FacorFunction(int x, int y)
        {
            int factorizedNumber = 21;
            int a = 8;

            if (x < factorizedNumber)
            {
                int result = x;
                for (int i = 0; i < y; i++)
                {
                    result = (result * a) % factorizedNumber;
                }
                return result;
            }
            else
                return x;
        }

        /// <summary>
        /// This is a helper function for TransformRegisterStateDirected. It can be used to map control index state to other states.
        /// It is not used directly in the factorization problem, so it basically does not change the given index.
        /// </summary>
        /// <param name="x">Control state index.</param>
        /// <returns>Mapped state index (here - leaves it unchanged).</returns>
        private int MapIndex(int x)
        {
            return x;
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

            register[0].TransformState(new PauliXGate());
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
