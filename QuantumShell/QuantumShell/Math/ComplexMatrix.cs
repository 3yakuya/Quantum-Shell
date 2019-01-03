using QuantumShell.QuantumModel;
using System;
using System.Collections.Generic;

namespace QuantumShell.Math
{
    class ComplexMatrix : IComplexMatrix
    {
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public IList<IList<IComplex>> Matrix { get; private set; }

        public ComplexMatrix(int rows, int columns)
        {
            this.RowCount = rows;
            this.ColumnCount = columns;
            this.Matrix = new List<IList<IComplex>>(RowCount);

            for (int i = 0; i < this.RowCount; i++)
            {
                List<IComplex> singleRow = new List<IComplex>(ColumnCount);
                for (int j = 0; j < ColumnCount; j++)
                    singleRow.Add(new Complex(0));
                this.Matrix.Add(singleRow);
            }
        }

        public ComplexMatrix() : this(1, 1) { }

        public IComplexMatrix IdentityMatrix(int size)
        {
            IComplexMatrix identityMatrix = new ComplexMatrix(size, size);
            for (int i = 0; i < size; i++)
            {
                identityMatrix.Matrix[i][i] = new Complex(1);
            }
            return identityMatrix;
        }
        
        public IComplexMatrix Dot(IComplexMatrix second)
        {
            if (this.ColumnCount != second.RowCount)
                throw (new ArgumentException("Incorrect matrix dimensions."));
            IComplexMatrix result = new ComplexMatrix(this.RowCount, second.ColumnCount);

            for (int columnInSecond = 0; columnInSecond < second.ColumnCount; columnInSecond++)
                for (int currentRow = 0; currentRow < this.RowCount; currentRow ++)
                    for (int currentColumn = 0; currentColumn < this.ColumnCount; currentColumn++)
                    {
                        IComplex toAdd = new Complex(this.Matrix[currentRow][currentColumn].Real, this.Matrix[currentRow][currentColumn].Imaginary);
                        toAdd.Multiply(second.Matrix[currentColumn][columnInSecond]);
                        result.Matrix[currentRow][columnInSecond].Add(toAdd);
                    }
            return result;
        }

        public IComplexMatrix Tensorize(IComplexMatrix second)
        {
            IComplexMatrix result = new ComplexMatrix(this.RowCount * second.RowCount, this.ColumnCount * second.ColumnCount);

            for (int firstRow = 0; firstRow < this.RowCount; firstRow++)
                for (int firstColumn = 0; firstColumn < this.ColumnCount; firstColumn++)
                    for (int secondRow = 0; secondRow < second.RowCount; secondRow++)
                        for (int secondColumn = 0; secondColumn < second.ColumnCount; secondColumn++)
                        {
                            int row = firstRow * second.RowCount + secondRow;
                            int column = firstColumn * second.ColumnCount + secondColumn;
                            IComplex product = new Complex(this.Matrix[firstRow][firstColumn].Real, this.Matrix[firstRow][firstColumn].Imaginary);
                            product.Multiply(second.Matrix[secondRow][secondColumn]);
                            result.Matrix[row][column] = product;
                        }
            return result;
        }
    }
}
