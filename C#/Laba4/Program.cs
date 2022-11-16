using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4
{
    internal class Program
    {
        static string GenPassword()
        {
            Random rng = new Random();

            Console.Write("Enter password lenght: ");
            int l = int.Parse(Console.ReadLine());
            
            Console.Write("Enter start range: ");
            int start = int.Parse(Console.ReadLine());
            Console.Write("Enter end range: ");
            int end = int.Parse(Console.ReadLine());

            string password = "";

            for (int i = 0; i < l; i++)
            {
                char ch = (char)rng.Next(start, end);
                password += ch;
            }

            return "password: " + password; 
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GenPassword());





            Console.ReadKey();
        }
    }
}
