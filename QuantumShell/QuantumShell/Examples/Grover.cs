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
            int registerSize = 3;

            QuantumBit[] register = InitializeQuantumRegister(registerSize);

            IQuantumGate H = new HadamardGate();
            IQuantumGate G = new SampleGroverOracle();
            IQuantumGate D = new GroverDiffusionGate();

            PeekRegister(register);
            QuantumSubroutine(register, G, D);
            QuantumSubroutine(register, G, D);
            PeekRegister(register);

            Console.ReadLine();
        }

        private void QuantumSubroutine(QuantumBit[] register, IQuantumGate G, IQuantumGate D)
        {
            Console.WriteLine("\nPerforming the quantum subroutine...\n");
            HadamardEntireState();
            register[0].TransformState(G);
            register[0].TransformState(D);
            HadamardEntireState();
        }

        private HadamardEntireState(QuantumBit[] register, HadamardGate H)
        {
            for (Qubit in register)
            {
                Qubit.TransformState(H);
            }
        }

        private void PeekRegister(QuantumBit[] register)
        {
            Console.WriteLine(register[0].Peek());
        }

        private void MeasureHighRegister(QuantumBit[] register)
        {
            Console.WriteLine("\nMeasured result:");
            for (int i = register.size;  i >= 0; i--) 
            {
                Console.Write(register[i].Measure());   
            }
            Console.WriteLine();
        }
    }
}
