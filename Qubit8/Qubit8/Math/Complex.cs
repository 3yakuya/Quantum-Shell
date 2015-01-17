using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubit8.Math
{
    class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public Complex(double real, double imaginary)
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        public Complex() : this(0, 0) { }

        public Complex(double realOnly) : this(realOnly, 0) { }

        public override string ToString()
        {
            if (this.Real == 0 && this.Imaginary == 0)
                return "0";

            string stringRepresentation = "";
            if (this.Real != 0)
            {
                stringRepresentation += this.Real.ToString();
            }

            if (this.Imaginary != 0)
            {
                if (this.Real != 0)
                    stringRepresentation += " + ";
                stringRepresentation += this.Imaginary.ToString() + "i";
            }
            return stringRepresentation;
        }

        public static Complex operator +(Complex one, Complex two)
        {
            return new Complex(one.Real + two.Real, one.Imaginary + two.Imaginary);
        }

        public static Complex operator -(Complex one, Complex two)
        {
            return new Complex(one.Real - two.Real, one.Imaginary - two.Imaginary);
        }

        public static Complex operator *(Complex one, Complex two)
        {
            double real = one.Real * two.Real - one.Imaginary * two.Imaginary;
            double imaginary = one.Imaginary * two.Real + one.Real * two.Imaginary;
            return new Complex(real, imaginary);
        }

        public static Complex Power(Complex number, int power)
        {
            Complex result = new Complex(number.Real, number.Imaginary);
            for (int i = 1; i < power; i++)
            {
                result *= number;
            }
            return result;
        }
    }
}
