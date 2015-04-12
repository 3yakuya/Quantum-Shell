using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
