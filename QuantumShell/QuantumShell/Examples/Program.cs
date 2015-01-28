using QuantumShell.Examples;
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
            //Factorization factorizationProblem = new Factorization();
            //factorizationProblem.OrderFindingQuantumSubroutine();

            //HiddenSubgroup hiddenSubgroupProblem = new HiddenSubgroup();
            //hiddenSubgroupProblem.HiddenSubgroupQuantumSubroutine();

            //DeutschJozsa deutschJozsaProblem = new DeutschJozsa();
            //deutschJozsaProblem.DeutschJozsaQuantumRoutine();

            Deutsch deutschProblem = new Deutsch();
            deutschProblem.DeutschQuantumRoutine();

            //Interpreter interpreter = new Interpreter();
            //interpreter.Run();
        }
    }
}
