using System.Collections.Generic;

namespace QuantumShell.QuantumModel
{
    public interface IQuantumProvider
    {
        IComplex Complex(double real, double imaginary, int precision);
        IComplex Complex(double real, double imaginary);
        IComplex Complex(double real);
        IComplex Complex();

        IComplexMatrix ComplexMatrix(int rows, int columns);
        IComplexMatrix ComplexMatrix();

        IList<QuantumBit> List(IList<QuantumBit> list);
        IList<QuantumBit> List();

        IRandomGenerator RandomGenerator();
    }
}
