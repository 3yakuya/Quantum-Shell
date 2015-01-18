using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8.Math
{
    class ComplexMatrix
    {
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public IList<IList<Complex>> Matrix { get; private set; }

        public ComplexMatrix(int rows, int columns)
        {
            this.RowCount = rows;
            this.ColumnCount = columns;
            this.Matrix = new List<IList<Complex>>(RowCount);

            for (int i = 0; i < this.RowCount; i++)
            {
                List<Complex> singleRow = new List<Complex>(ColumnCount);
                for (int j = 0; j < ColumnCount; j++)
                    singleRow.Add(new Complex(0));
                this.Matrix.Add(singleRow);
            }
        }

        public ComplexMatrix Dot(ComplexMatrix second)
        {
            if (this.ColumnCount != second.RowCount)
                throw (new ArgumentException());
            ComplexMatrix result = new ComplexMatrix(this.RowCount, second.ColumnCount);

            for (int columnInSecond = 0; columnInSecond < second.ColumnCount; columnInSecond++)
                for (int currentRow = 0; currentRow < this.RowCount; currentRow ++)
                    for (int currentColumn = 0; currentColumn < this.ColumnCount; currentColumn++)
                    {
                        result.Matrix[currentRow][columnInSecond] += this.Matrix[currentRow][currentColumn]*second.Matrix[currentColumn][columnInSecond];
                    }
            return result;
        }

        public ComplexMatrix Tensorize(ComplexMatrix second)
        {
            ComplexMatrix result = new ComplexMatrix(this.RowCount * second.RowCount, this.ColumnCount * second.ColumnCount);

            for (int firstRow = 0; firstRow < this.RowCount; firstRow++)
                for (int firstColumn = 0; firstColumn < this.ColumnCount; firstColumn++)
                    for (int secondRow = 0; secondRow < second.RowCount; secondRow++)
                        for (int secondColumn = 0; secondColumn < second.ColumnCount; secondColumn++)
                        {
                            int row = firstRow*this.RowCount + secondRow;
                            int column = firstColumn*this.ColumnCount + secondColumn;
                            result.Matrix[row][column] = this.Matrix[firstRow][firstColumn] * second.Matrix[secondRow][secondColumn];
                        }
            return result;
        }
    }
}
