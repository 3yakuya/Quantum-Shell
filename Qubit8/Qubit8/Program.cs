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

            ComplexMatrix one = new ComplexMatrix(2, 2);
            one.Matrix[0][0] = new Complex(-1);
            one.Matrix[0][1] = new Complex(3);
            one.Matrix[1][0] = new Complex(4);
            one.Matrix[1][1] = new Complex(5);

            ComplexMatrix two = new ComplexMatrix(2, 2);
            two.Matrix[0][0] = new Complex(1);
            two.Matrix[0][1] = new Complex(0);
            two.Matrix[1][0] = new Complex(2);
            two.Matrix[1][1] = new Complex(-1);

            ComplexMatrix matrix = new ComplexMatrix().IdentityMatrix(8);

            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    Console.Write(matrix.Matrix[i][j] + "\t");
                }
                Console.Write("\n");
            }
            Console.ReadLine();
        }
    }
}
