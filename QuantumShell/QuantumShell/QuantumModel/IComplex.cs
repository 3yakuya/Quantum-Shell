namespace QuantumShell.QuantumModel
{
    public interface IComplex
    {
        double Real { get; set; }
        double Imaginary { get; set; }

        void Add(IComplex number);
        void Substract(IComplex number);
        void Multiply(IComplex number);
        void Divide(IComplex number);
        bool EqualTo(IComplex number);
        double Absolute();
        IComplex Power(int power);
    }
}
