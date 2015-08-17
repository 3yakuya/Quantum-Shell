using QuantumShell.QuantumModel;
using System.Collections.Generic;

namespace QuantumShell.Math
{
    public class ComplexProvider : IQuantumProvider
    {
        public IComplex Complex(double real, double imaginary, int precision)
        {
            return new Complex(real, imaginary, precision);
        }

        public IComplex Complex(double real, double imaginary)
        {
            return new Complex(real, imaginary);
        }

        public IComplex Complex(double real)
        {
            return new Complex(real);
        }

        public IComplex Complex()
        {
            return new Complex();
        }

        public IComplexMatrix ComplexMatrix(int rows, int columns) {
            return new ComplexMatrix(rows, columns);
        }

        public IComplexMatrix ComplexMatrix()
        {
            return new ComplexMatrix();
        }

        public IList<QuantumBit> List(IList<QuantumBit> list)
        {
            return new List<QuantumBit>(list);
        }

        public IList<QuantumBit> List()
        {
            return new List<QuantumBit>();
        }

        public IRandomGenerator RandomGenerator()
        {
            return new RandomGenerator();
        }
    }
}
