using QuantumShell.Math;
using QuantumShell.QuantumGates;
using QuantumShell.QuantumGates.RotationGates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuantumShell.Services
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

        int qubitReverseIndex = 7;

        public Interpreter()
        {
            ResetRegister();

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
            bool scriptMode = false;
            bool scriptExecute = false;

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
            string ControlledPattern = @"c-[H|X|Y|Z|T|R2|R3|R4]\(\d,\d\)";

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
            Regex ControlledRegex = new Regex(ControlledPattern);

            StreamReader reader = null;

            Console.WriteLine("Quantum Shell (c) 2015 by Jakub Pilch");
            Console.WriteLine("Type \"help\" to see possible commands.");

            while (isRunning)
            {
                Console.Write("-> ");
                string command;
                if (!scriptMode)
                    command = Console.ReadLine();
                else
                {
                    if (!scriptExecute)
                    {
                        string scriptCommand = Console.ReadLine();
                        if (scriptCommand == "exit")
                            break;
                        if (scriptCommand == "run")
                            scriptExecute = true;
                    }
                    command = reader.ReadLine();
                    Console.Write(command + ": ");
                }

                if (HRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    try
                    {
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(H);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(X);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(I);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(Y);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(Z);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(T);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(R2);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(R3);
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
                        QuantumRegister[qubitReverseIndex - qubitNumber].TransformState(R4);

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
                    Console.WriteLine(QuantumRegister[qubitReverseIndex - qubitNumber].Peek() + "\n");
                }
                else if (MeasureRegex.IsMatch(command))
                {
                    int qubitNumber = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    Console.WriteLine("Measuring " + qubitNumber + " qubit:");
                    Console.WriteLine(QuantumRegister[qubitReverseIndex - qubitNumber].Measure() + "\n");
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
                    QuantumRegister[qubitReverseIndex - qubit].ResetState();
                }
                else if (ControlledRegex.IsMatch(command))
                {
                    string operation = command.Split('-')[1].ElementAt(0).ToString();
                    string secondOperationChar = command.Split('-')[1].ElementAt(1).ToString();
                    if (secondOperationChar != "(")
                    {
                        operation += secondOperationChar;
                    }
                    int target = int.Parse(command.Split('(')[1].ElementAt(0).ToString());
                    int control = int.Parse(command.Split('(')[1].ElementAt(2).ToString());
                    ControlledOperation(control, target, operation.ToString());
                }
                else
                {
                    switch (command)
                    {
                        case "Reset":
                            ResetRegister();
                            break;

                        case "help":
                            ShowHelp();
                            break;

                        case "exit":
                            isRunning = false;
                            Console.WriteLine("Thanks for usage! Press any key to quit.");
                            Console.ReadLine();
                            break;

                        case "load":
                            Console.WriteLine("Type in a script path:\n");
                            string path = Console.ReadLine();
                            reader = LoadScript(path);
                            if (reader != null)
                            {
                                Console.WriteLine("Entering script step-by-step mode.");
                                Console.WriteLine("Type \"exit\" to quit or \"run\" to run continuously.");
                                scriptMode = true;
                            }
                            break;

                        default:
                            Console.WriteLine("Unsupported operation. Type \"help\" to see available commands.");
                            break;
                    }
                }
            }
        }

        private void ShowHelp()
        {
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
            Console.WriteLine("\t->c-G(control,target) (G can be any gate from above).");
            Console.WriteLine("\t->Peek(qubitNumber)");
            Console.WriteLine("\t->Measure(qubitNumber)");
            Console.WriteLine("\t->Join(fromQubit-toQubit)");
            Console.WriteLine("\t->Reset(qubitNumber)");
            Console.WriteLine("\t->Reset");
            Console.WriteLine("\t->exit");
            Console.WriteLine("\t->help");
            Console.WriteLine("\nRemember: qubits are numbered from right to left.");
            Console.WriteLine("Qubits are indexed from 0 to 7.");
        }
        private void ResetRegister()
        {
            Console.WriteLine("\nResetting the quantum register...\n");
            QuantumRegister = new Qubit[8];
            for (int i = 0; i < 8; i++)
                QuantumRegister[i] = new Qubit(7 - i);
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
                    QuantumRegister[qubitReverseIndex - qubitIndex].JoinState(QuantumRegister[qubitReverseIndex - qubitIndex - 1]);
            }
            else
            {
                for (int qubitIndex = to; qubitIndex < from; qubitIndex++)
                    QuantumRegister[qubitReverseIndex - qubitIndex].JoinState(QuantumRegister[qubitReverseIndex - qubitIndex - 1]);
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
            if (QuantumRegister[qubitReverseIndex - qubitIndex].StateQubitList.Count > 1)
            {
                Console.WriteLine("Selected qubit is possibly in an entangled state (it can't be set explicitly).");
                return;
            }

            ComplexMatrix state = new ComplexMatrix(1, 2);
            state.Matrix[0][value].Real = 1;
            QuantumRegister[qubitReverseIndex - qubitIndex].SetState(state);
        }

        private void ControlledOperation(int controlQubit, int targetQubit, string operation)
        {
            if (controlQubit > 7 || controlQubit < 0 || targetQubit > 7 || targetQubit < 0 || targetQubit == controlQubit)
            {
                Console.WriteLine("Qubit can not be used to control itself.");
                return;
            }

            controlQubit = qubitReverseIndex - controlQubit;
            targetQubit = qubitReverseIndex - targetQubit;
            try
            {
                switch (operation)
                {
                    case "H":
                        QuantumRegister[targetQubit].TransformStateControlled(H, QuantumRegister[controlQubit]);
                        break;

                    case "X":
                        QuantumRegister[targetQubit].TransformStateControlled(X, QuantumRegister[controlQubit]);
                        break;

                    case "I":
                        QuantumRegister[targetQubit].TransformStateControlled(I, QuantumRegister[controlQubit]);
                        break;

                    case "Y":
                        QuantumRegister[targetQubit].TransformStateControlled(Y, QuantumRegister[controlQubit]);
                        break;

                    case "Z":
                        QuantumRegister[targetQubit].TransformStateControlled(Z, QuantumRegister[controlQubit]);
                        break;

                    case "T":
                        QuantumRegister[targetQubit].TransformStateControlled(T, QuantumRegister[controlQubit]);
                        break;

                    case "R2":
                        QuantumRegister[targetQubit].TransformStateControlled(R2, QuantumRegister[controlQubit]);
                        break;

                    case "R3":
                        QuantumRegister[targetQubit].TransformStateControlled(R3, QuantumRegister[controlQubit]);
                        break;

                    case "R4":
                        QuantumRegister[targetQubit].TransformStateControlled(R4, QuantumRegister[controlQubit]);
                        break;

                    default:
                        Console.WriteLine("Unsupported gate.");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Only qubits in a consistent state may be used in a controlled operation.");
                Console.WriteLine("Consistent states may not contain any gaps.");
                return;
            }
        }

        private StreamReader LoadScript(string path)
        {
            try
            {
                return new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
