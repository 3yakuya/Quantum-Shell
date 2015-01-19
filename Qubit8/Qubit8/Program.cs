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
            //ComplexMatrix one = new ComplexMatrix().IdentityMatrix(4);
            //ComplexMatrix two = new ComplexMatrix(2,2);
            //two.Matrix[0][1] = new Complex(8);
            //two.Matrix[1][0] = new Complex(7);
            //two = one.Tensorize(two);

            //for (int i = 0; i < two.RowCount; i++)
            //{
            //    for (int j = 0; j < two.ColumnCount; j++ )
            //    {
            //        Console.Write(two.Matrix[i][j] + "\t");
            //    }
            //    Console.WriteLine();
            //}


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

            qubit.EntangleWith(qubit);
            qubit2.EntangleWith(qubit3);
            qubit3.EntangleWith(qubit);

            //Console.WriteLine(qubit.Measure());
            //Console.WriteLine();
            //foreach (var value in qubit.StateVector.Matrix)
            //    Console.Write(value + "\t");

            //Console.WriteLine();
            //Console.WriteLine(qubit3.Peek());
            //Console.ReadLine();

            Complex one = new Complex(4, 5.6);
            Complex two = new Complex(2, -4.1);
            Console.WriteLine(one / two);
            Console.ReadLine();
        }
    }
}
