using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.Examples
{
    /// <summary>
    /// This class provides methods to show the possible usage of the Quantum Shell for the search problem.
    /// </summary>
    class GroverSearch
    {
        public void GroverSearchQuantumRoutine()
        {
            int registerSize = 4;

            QuantumBit[] register = InitializeQuantumRegister(registerSize);

            IQuantumGate H = new HadamardGate();
            IQuantumGate G = new SampleGroverOracle();
            IQuantumGate D = new GroverDiffusionGate();
            IQuantumGate X = new PauliXGate();

            register[0].TransformState(X);
            register[0].TransformState(H);

            PeekRegister(register);
            HadamardEntireState(register, H);
            PeekRegister(register);
            QuantumSubroutine(register, H, G, D);
            QuantumSubroutine(register, H, G, D);

            MeasureHighRegister(register);

            Console.ReadLine();
        }

        private QuantumBit[] InitializeQuantumRegister(int size)
        {
            QuantumBit[] register = new Qubit[size];
            IQuantumProvider provider = new ComplexProvider();
            register[0] = new Qubit(0, provider);
            for (int i = 1; i < size; i++)
            {
                register[i] = new Qubit(i, provider);
                register[i].JoinState(register[i - 1]);
            }
            return register;
        }

        private void QuantumSubroutine(QuantumBit[] register, IQuantumGate H, IQuantumGate G, IQuantumGate D)
        {
            Console.WriteLine("\nPerforming the quantum subroutine...\n");
            register[0].TransformState(G);
            PeekRegister(register);
            register[1].TransformState(D);
            PeekRegister(register);
            Console.WriteLine();
        }

        private void HadamardEntireState(QuantumBit[] register, IQuantumGate H)
        {
            for (int qubitIndex = 1; qubitIndex < register.Length; qubitIndex++)
            {
                register[qubitIndex].TransformState(H);
            }
        }

        private void PeekRegister(QuantumBit[] register)
        {
            Console.WriteLine(register[0].Peek());
        }

        private void MeasureHighRegister(QuantumBit[] register)
        {
            Console.WriteLine("\nMeasured result:");
            for (int i = register.Length - 1;  i > 0; i--) 
            {
                Console.Write(register[i].Measure());   
            }
            Console.WriteLine();
        }
    }
}
