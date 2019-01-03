using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.Examples
{
    /// <summary>
    /// This class provides methods to show the possible usage of the Quantum Shell for the search problem.
    /// Running Grover Search algorithm finding solution to SampleGroverOracle.SampleOracleFunction.
    /// </summary>
    class GroverSearch
    {
        public void GroverSearchQuantumRoutine()
        {
            int registerSize = 9;
            QuantumBit[] register = InitializeQuantumRegister(registerSize);

            IQuantumGate H = new HadamardGate();
            IQuantumGate G = new SampleGroverOracle();
            IQuantumGate D = new GroverDiffusionGate();
            IQuantumGate X = new PauliXGate();

            PrepareAncillaQubit(register, H, X);
            HadamardEntireState(register, H);

            int repetitionCount = (int) System.Math.Ceiling(System.Math.Sqrt(registerSize)) + 1;

            for (int repetition = 0; repetition < repetitionCount; repetition++)
            {
                QuantumSubroutine(register, H, G, D);
            }

            Console.WriteLine(register[0].Peek());

            MeasureHighRegister(register);
            Console.ReadLine();
        }

        private void PrepareAncillaQubit(QuantumBit[] register, IQuantumGate H, IQuantumGate X)
        {
            Console.WriteLine("Preparing ancilla qubit...");
            register[0].TransformState(X);
            register[0].TransformState(H);
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
            Console.WriteLine("Applying Oracle gate...");
            register[0].TransformState(G);
            Console.WriteLine("Applying Diffusion gate...");
            register[1].TransformState(D);
            Console.WriteLine();
        }

        private void HadamardEntireState(QuantumBit[] register, IQuantumGate H)
        {
            Console.WriteLine("Applying Hadamard gates to all target qubits...");
            for (int qubitIndex = 1; qubitIndex < register.Length; qubitIndex++)
            {
                register[qubitIndex].TransformState(H);
            }
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
