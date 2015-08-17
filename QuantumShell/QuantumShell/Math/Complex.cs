using QuantumShell.QuantumModel;
using System;

namespace QuantumShell.Math
{
    class Complex : IComplex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        private static int DefaultPrecision
        {
            get
            {
                return 10;
            }
        }

        public Complex(double real, double imaginary, int precision)
        {
            double errorCorrector = System.Math.Pow(10, -1 * precision);
            this.Real = real;
            this.Imaginary = imaginary;
            if (System.Math.Abs(Real) < errorCorrector)
                Real = 0;
            if (System.Math.Abs(Imaginary) < errorCorrector)
                Imaginary = 0;        
        }

        public Complex(double real, double imaginary) : this(real, imaginary, DefaultPrecision) { }

        public Complex() : this(0, 0, DefaultPrecision) { }

        public Complex(double realOnly) : this(realOnly, 0, DefaultPrecision) { }

        public void Add(IComplex number)
        {
            Complex result = this + (Complex)number;
            this.Real = result.Real;
            this.Imaginary = result.Imaginary;
        }

        public void Substract(IComplex number)
        {
            Complex result = this - (Complex)number;
            this.Real = result.Real;
            this.Imaginary = result.Imaginary;
        }

        public void Multiply(IComplex number)
        {
            Complex result = this * (Complex)number;
            this.Real = result.Real;
            this.Imaginary = result.Imaginary;
        }

        public void Divide(IComplex number)
        {
            Complex result = this / (Complex)number;
            this.Real = result.Real;
            this.Imaginary = result.Imaginary;
        }

        public bool EqualTo(IComplex number)
        {
            return this == (Complex)number;
        }

        public double Absolute()
        {
            return Complex.Absolute(this);
        }

        public void Power(int power)
        {
            Complex result = Complex.Power(this, power);
            this.Real = result.Real;
            this.Imaginary = result.Imaginary;
        }

        public override string ToString()
        {
            if (this.Real == 0 && this.Imaginary == 0)
                return "0";

            string stringRepresentation = "(";
            if (this.Real != 0)
            {
                stringRepresentation += this.Real.ToString("0.####");
            }

            if (this.Imaginary != 0)
            {
                if (this.Real != 0)
                    stringRepresentation += " + ";
                stringRepresentation += this.Imaginary.ToString("0.####") + "i";
            }
            stringRepresentation += ")";
            return stringRepresentation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (!(obj is Complex))
                return false;
            Complex complexObj = obj as Complex;
            if (this.Real != complexObj.Real)
                return false;
            if (this.Imaginary != complexObj.Imaginary)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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

        public static Complex operator /(Complex one, Complex two)
        {
            if (two.Real == 0 && two.Imaginary == 0)
                throw new ArgumentException();

            double real = (one.Real * two.Real + one.Imaginary * two.Imaginary) /
                (two.Real * two.Real + two.Imaginary * two.Imaginary);
            double imaginary = (one.Imaginary * two.Real - one.Real * two.Imaginary) /
                (two.Real * two.Real + two.Imaginary * two.Imaginary);
            Complex result = new Complex(real, imaginary);
            return result;
        }

        public static bool operator ==(Complex one, Complex two)
        {
            if (one.Real == two.Real && one.Imaginary == two.Imaginary)
                return true;
            return false;
        }

        public static bool operator !=(Complex one, Complex two)
        {
            if (one.Real != two.Real || one.Imaginary != two.Imaginary)
                return true;
            return false;
        }

        public static double Absolute(Complex number)
        {
            double x = number.Real * number.Real;
            double y = number.Imaginary * number.Imaginary;
            return System.Math.Sqrt(x + y);
        }

        public static Complex Power(Complex number, int power)
        {
            if (power < 0)
                throw new ArgumentException("Only 0 or positive powers can be calulated.");
            if (power == 0)
                return new Complex(1);

            Complex result = new Complex(number.Real, number.Imaginary);
            for (int i = 1; i < power; i++)
            {
                result *= number;
            }
            return result;
        }
    }
}
