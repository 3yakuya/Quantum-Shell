using Qubit8.Math;
using Qubit8.QuantumGates;
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
            //ComplexMatrix one = new ComplexMatrix().IdentityMatrix(4);
            //ComplexMatrix two = new ComplexMatrix(2,2);
            //two.Matrix[0][1] = new Complex(8);
            //two.Matrix[1][0] = new Complex(7);
            //two = one.Tensorize(two);

            //for (int i = 0; i < two.RowCount; i++)
            //{
            //    for (int j = 0; j < two.ColumnCount; j++ )
            //    {
            //        Console.Write(two.Matrix[i][j] + "\t");
            //    }
            //    Console.WriteLine();
            //}


            Qubit qubit = new Qubit();
            ComplexMatrix stateArray = new ComplexMatrix(1, 2);
            stateArray.Matrix[0][0].Real = 0;
            stateArray.Matrix[0][1].Real = 1;

            HadamardGate H = new HadamardGate();
            PauliXGate X = new PauliXGate();
            CNOTGate CNOT = new CNOTGate();

            //qubit.TransformState(H);

            Qubit qubit2 = new Qubit();
            //qubit2.TransformState(H);

            Qubit qubit3 = new Qubit();
            //qubit3.TransformState(H);

            qubit.SetState(stateArray);
            qubit2.SetState(stateArray);
            qubit3.SetState(stateArray);

            qubit.JoinState(qubit2);
            qubit2.JoinState(qubit3);
            qubit3.JoinState(qubit);
            qubit.JoinState(qubit);
            Console.WriteLine(qubit.Peek());
            Console.WriteLine();

            Console.WriteLine("-----------------------CNOT1----------------------");
            qubit3.TransformStateControlled(X, qubit2);
            Console.WriteLine(qubit.Peek());
            Console.WriteLine();

            //Console.WriteLine("-----------------------X1----------------------");
            //qubit.TransformState(X);
            //Console.WriteLine(qubit.Peek());
            //Console.WriteLine();

            //Console.WriteLine("-----------------------X2----------------------");
            //qubit2.TransformState(X);
            //Console.WriteLine(qubit2.Peek());
            //Console.WriteLine();

            //Console.WriteLine("-----------------------X3----------------------");
            //qubit3.TransformState(X);
            //Console.WriteLine(qubit3.Peek());
            //Console.WriteLine();

            //Console.WriteLine("-----------------------Hadamard1----------------------");
            //qubit.TransformState(H);
            //Console.WriteLine(qubit.Peek());
            //Console.WriteLine();

            //Console.WriteLine("-----------------------Hadamard2----------------------");
            //qubit2.TransformState(H);
            //Console.WriteLine(qubit2.Peek());
            //Console.WriteLine();

            //Console.WriteLine("-----------------------Hadamard3----------------------");
            //qubit3.TransformState(H);
            //Console.WriteLine(qubit3.Peek());
            //Console.WriteLine();

            //Console.WriteLine(qubit.Measure());
            //Console.WriteLine();
            //Console.WriteLine(qubit2.Peek());

            //Console.WriteLine(qubit2.Measure());
            //Console.WriteLine();
            //Console.WriteLine(qubit3.Peek());

            //Console.WriteLine(qubit3.Measure());
            //Console.WriteLine();
            //Console.WriteLine(qubit.Peek());

            //qubit.ResetState();
            //Console.WriteLine();
            //Console.WriteLine(qubit.Peek());
            //Console.WriteLine(qubit2.Peek());
            //Console.WriteLine(qubit3.Peek());

            Console.ReadLine();
        }
    }
}
