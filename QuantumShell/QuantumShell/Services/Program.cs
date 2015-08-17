using QuantumShell.Services;

namespace QuantumShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Interpreter interpreter = new Interpreter();
            interpreter.Run();
        }
    }
}
