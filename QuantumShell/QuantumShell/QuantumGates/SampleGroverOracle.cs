
using QuantumShell.Math;
using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.QuantumGates
{
    /// <summary>
    /// Sample oracle for Grover Search algorithm to find solutions for SampleOracleFunction.
    /// Prepared in traditional notation (utilizing qubit indexed 0 as ancilla qubit.)
    /// </summary>
    class SampleGroverOracle : IQuantumGate
    {
        public override int QubitCount { get; protected set; }
        public override IComplexMatrix Transform { get; protected set; }

        public SampleGroverOracle()
        {
            this.QubitCount = 9;
            int stateSize = power(2, this.QubitCount);
            this.Transform = new ComplexMatrix(stateSize, stateSize);
            for (int row = 0; row < stateSize; row++)
            {
                for (int col = 0; col < stateSize; col++)
                {
                    if (row == col)
                    {
                        this.Transform.Matrix[row][col].Real = power(-1, SampleOracleFunction(row/2));
                    }
                    else
                    {
                        this.Transform.Matrix[row][col].Real = 0;
                    }
                }
            }
        }

        private int SampleOracleFunction(int argument)
        {
            return argument - 173 == 0 ? 1 : 0;
        }

        private int power(int number, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= number;
            }

            return result;
        }
    }
}
