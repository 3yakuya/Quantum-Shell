using Qubit8.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8
{
    class Program
    {
        static void Main(string[] args)
        {
            Qubit qubit = new Qubit();
            ComplexMatrix stateArray = new ComplexMatrix(1, 2);
            stateArray.Matrix[0][0] = new Complex(0.7);
            stateArray.Matrix[0][1] = new Complex(0.7);
            qubit.StateVector = stateArray;

            Qubit qubit2 = new Qubit();
            ComplexMatrix stateArray2 = new ComplexMatrix(1, 2);
            stateArray2.Matrix[0][0] = new Complex(0.7);
            stateArray2.Matrix[0][1] = new Complex(0.7);
            qubit2.StateVector = stateArray2;

            Qubit qubit3 = new Qubit();
            ComplexMatrix stateArray3 = new ComplexMatrix(1, 2);
            stateArray3.Matrix[0][0] = new Complex(0.7);
            stateArray3.Matrix[0][1] = new Complex(0.7);
            qubit3.StateVector = stateArray2;

            qubit.EntangleWith(qubit2);
            qubit2.EntangleWith(qubit3);

            //Console.WriteLine(qubit.Measure());
            //Console.WriteLine();
            //foreach (var value in qubit.StateVector)
            //    Console.Write(value + "\t");

            Console.WriteLine();
            Console.WriteLine(qubit.Peek());
            Console.ReadLine();
        }
    }
}
