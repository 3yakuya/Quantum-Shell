using QuantumShell.Math;
using QuantumShell.QuantumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumShell.QuantumGates
{
    class InverseQuantumFourierTransform : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }

        public InverseQuantumFourierTransform(int qubitCount)
        {
            this.QubitCount = qubitCount;
            int size = (int)System.Math.Pow(2, qubitCount);
            Transform = new ComplexMatrix(size, size);
            Complex omega = GetOmega(size);
            Complex omegaFactor = new Complex(1 / System.Math.Sqrt(size));

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Transform.Matrix[row][column] = omegaFactor * Complex.Power(omega, row * column);
                }
            }
        }

        private Complex GetOmega(int size)
        {
            Complex i = new Complex(0, 1);
            double PI2 = 2 * System.Math.PI;

            double real = System.Math.Cos(-PI2 / size);
            double imaginary = System.Math.Sin(-PI2 / size);
            Complex omega = new Complex(real, imaginary);

            return omega;
        }
    }
}
