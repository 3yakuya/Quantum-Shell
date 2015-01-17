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
            ComplexMatrix matrix = new ComplexMatrix(3, 3);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    matrix.Matrix[i][j] = Complex.Power(new Complex(0.3, 2), 3);
                }
            }

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
