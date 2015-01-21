using Qubit8.Math;
using Qubit8.QuantumGates;
using Qubit8.QuantumGates.RotationGates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Qubit8.Services
{
    class Interpreter
    {
        private Qubit[] QuantumRegister { get; set; }

        private HadamardGate H { get; set; }
        private PauliXGate X { get; set; }
        private PauliIGate I { get; set; }
        private PauliYGate Y { get; set; }
        private PauliZGate Z { get; set; }
        private Pi8Gate T { get; set; }
        private R2ReversedGate R2 { get; set; }
        private R3ReversedGate R3 { get; set; }
        private R4ReversedGate R4 { get; set; }

        public Interpreter()
        {
            QuantumRegister = new Qubit[8];
            for (int i = 0; i < 8; i++)
                QuantumRegister[i] = new Qubit(i);

                H = new HadamardGate();
            X = new PauliXGate();
            I = new PauliIGate();
            Y = new PauliYGate();
            Z = new PauliZGate();
            T = new Pi8Gate();
            R2 = new R2ReversedGate();
            R3 = new R3ReversedGate();
            R4 = new R4ReversedGate();
        }

        public void Run()
        {
            bool isRunning = true;

            string HPattern = @"H\(\d\)";
            string XPattern = @"X\(\d\)";
            string IPattern = @"I\(\d\)";
            string YPattern = @"Y\(\d\)";
            string ZPattern = @"Z\(\d\)";
            string TPattern = @"T\(\d\)";
            string R2Pattern = @"R2\(\d\)";
            string R3Pattern = @"R3\(\d\)";
            string R4Pattern = @"R4\(\d\)";
            string PeekPattern = @"Peek\(\d\)";
            string MeasurePattern = @"Measure\(\d\)";
            string JoinPattern = @"Join\(\d-\d\)";
            string SetPattern = @"Set\(\d,\d\)";
            string ResetPattern = @"Reset\(\d\)";

            Regex HRegex = new Regex(HPattern);
            Regex XRegex = new Regex(XPattern);
            Regex IRegex = new Regex(IPattern);
            Regex YRegex = new Regex(YPattern);
            Regex ZRegex = new Regex(ZPattern);
            Regex TRegex = new Regex(TPattern);
            Regex R2Regex = new Regex(R2Pattern);
            Regex R3Regex = new Regex(R3Pattern);
            Regex R4Regex = new Regex(R4Pattern);
            Regex PeekRegex = new Regex(PeekPattern);
            Regex MeasureRegex = new Regex(MeasurePattern);
            Regex JoinRegex = new Regex(JoinPattern);
            Regex SetRegex = new Regex(SetPattern);
            Regex ResetRegex = new Regex(ResetPattern);

            Console.WriteLine("Qubit8 (c) 2015 by Jakub Pilch");
            Console.WriteLine("Type \"help\" to see possible commands.");

            while (isRunning)
            {
                Console.Write("-> ");
                string command = Console.ReadLine();
                if (HRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(H);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (XRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(X);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (IRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(I);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (YRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(Y);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (ZRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(Z);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (TRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(T);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (R2Regex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(R2);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (R3Regex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(R3);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (R4Regex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitNumber].TransformState(H);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An unexpected error ocurred.");
                    }
                }
                else if (PeekRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    Console.WriteLine("Revealing " + qubitNumber + " qubit:");
                    Console.WriteLine(QuantumRegister[qubitNumber].Peek() + "\n");
                }
                else if (MeasureRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    Console.WriteLine("Measuring " + qubitNumber + " qubit:");
                    Console.WriteLine(QuantumRegister[qubitNumber].Measure() + "\n");
                }
                else if (JoinRegex.IsMatch(command))
                {
                    int qubitOne = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    int qubitTwo = int.Parse(command.Split('(')[1].ElementAt(2).ToString());
                    JoinQubitsInRegister(qubitOne, qubitTwo);
                }
                else if (SetRegex.IsMatch(command))
                {
                    int qubit = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    int state = int.Parse(command.Split('(')[1].ElementAt(2).ToString());
                    SetQubit(qubit, state);
                }
                else if (ResetRegex.IsMatch(command))
                {
                    int qubit = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    Console.WriteLine("\nThis will reset all qubits in consistent state with the selected one.\n");
                    QuantumRegister[qubit].ResetState();
                }
                else
                {
                    switch (command)
                    {
                        case "Reset":
                            ResetRegister();
                            break;

                        case "help":
                            Console.WriteLine("Available commands:");
                            Console.WriteLine("\t->H(qubitNumber)");
                            Console.WriteLine("\t->X(qubitNumber)");
                            Console.WriteLine("\t->I(qubitNumber)");
                            Console.WriteLine("\t->Y(qubitNumber)");
                            Console.WriteLine("\t->Z(qubitNumber)");
                            Console.WriteLine("\t->T(qubitNumber)");
                            Console.WriteLine("\t->R2(qubitNumber)");
                            Console.WriteLine("\t->R3(qubitNumber)");
                            Console.WriteLine("\t->R4(qubitNumber)");
                            Console.WriteLine("\t->Peek(qubitNumber)");
                            Console.WriteLine("\t->Measure(qubitNumber)");
                            Console.WriteLine("\t->Join(fromQubit-toQubit)");
                            Console.WriteLine("\t->Reset(qubitNumber)");
                            Console.WriteLine("\t->Reset");
                            Console.WriteLine("\t->exit");
                            Console.WriteLine("\t->help");
                            Console.WriteLine("\nRemember: qubits are numbered from right to left.");
                            Console.WriteLine("Qubits are indexed from 0 to 7.");
                            break;

                        case "exit":
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Unsupported operation. Type \"help\" to see available commands.");
                            break;
                    }
                }
            }
        }

        private void ResetRegister()
        {
            Console.WriteLine("\nResetting the quantum register...\n");
            this.QuantumRegister = new Qubit[8];
            for (int i = 0; i < 8; i++)
                QuantumRegister[i] = new Qubit(i);
        }

        private void JoinQubitsInRegister(int from, int to)
        {
            if (from > 7 || from < 0 || to > 7 || to < 0 || from == to)
            {
                Console.WriteLine("\nIncorrect range for join.");
                return;
            }
            
            if (from < to)
            {
                for (int qubitIndex = from; qubitIndex < to; qubitIndex++)
                    QuantumRegister[qubitIndex].JoinState(QuantumRegister[qubitIndex + 1]);
            }
            else
            {
                for (int qubitIndex = to; qubitIndex < from; qubitIndex++)
                    QuantumRegister[qubitIndex].JoinState(QuantumRegister[qubitIndex + 1]);
            }
        }

        private void SetQubit(int qubitIndex, int value)
        {
            if (value != 0 && value != 1)
            {
                Console.WriteLine("Incorrect value (only 0 or 1 is possible).");
                return;
            }
            if (qubitIndex > 7 || qubitIndex < 0)
            {
                Console.WriteLine("Qubit index not in [0, 7].");
                return;
            }
            if (QuantumRegister[qubitIndex].StateQubitList.Count > 1)
            {
                Console.WriteLine("Selected qubit is possibly in an entangled state (it can't be set explicitly).");
                return;
            }

            ComplexMatrix state = new ComplexMatrix(1, 2);
            state.Matrix[0][value].Real = 1;
            QuantumRegister[qubitIndex].SetState(state);
        }
    }
}
