using System;
using System.Collections.Generic;
using System.IO;
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
        // генерим пaроль


        static void Main(string[] args)
        {
            //Console.WriteLine(GenPassword());



            Console.WriteLine("Enter text:");
            string text = Console.ReadLine();

            Console.Write("Enter shift:");
            int sh = int.Parse(Console.ReadLine());

            // шифрование по цезарю
            char[] encoded = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
                encoded[i] = (char)((((byte)text[i]) + sh) % 256);

            // дешифрование по цезарю
            char[] decoded = new char[text.Length];

            for (int i = 0; i < encoded.Length; i++)
            {
                int t = (int)((byte)(encoded[i] - sh));

                if (t < 0) t += 255;

                decoded[i] = (char)t;
            }

            // вывод на экран
            for (int i = 0; i < encoded.Length; i++)
                Console.Write(encoded[i]);
            Console.WriteLine();
            for (int i = 0; i < decoded.Length; i++)
                Console.Write(decoded[i]);

            Console.ReadKey();
        }
    }
}
