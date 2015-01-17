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
            Qubit qubit = new Qubit();
            Console.WriteLine(qubit.Peek());
            Console.ReadLine();
        }
    }
}
