using System.Collections.Generic;

namespace QuantumShell.QuantumModel
{
    public interface IComplexMatrix
    {
        int RowCount { get; }
        int ColumnCount { get;}
        IList<IList<IComplex>> Matrix { get;}

        IComplexMatrix IdentityMatrix(int size);
        IComplexMatrix Dot(IComplexMatrix second);
        IComplexMatrix Tensorize(IComplexMatrix second);
    }
}
