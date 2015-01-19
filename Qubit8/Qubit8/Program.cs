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
            Complex[] stateArray = new Complex[8];
            stateArray[0] = new Complex(0);
            stateArray[1] = new Complex(0);
            stateArray[2] = new Complex(0.2);
            stateArray[3] = new Complex(0.2);
            stateArray[4] = new Complex(0.2);
            stateArray[5] = new Complex(0);
            stateArray[6] = new Complex(0.2);
            stateArray[7] = new Complex(0.2);
            qubit.StateVector = stateArray;

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
