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

        static string Encoded(string s, int sh)
        {
            string result = "";

            char[] encoded = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
                encoded[i] = (char)((((byte)s[i]) + sh) % 256);

            for (int i = 0; i < s.Length; i++)
                result = result + encoded[i];

            return result;
        }
        // шифрование по цезарю

        static string Decoded(string s, int sh)
        {
            string result = "";

            char[] decoded = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                int t = (int)((byte)(s[i] - sh));

                if (t < 0) t += 255;

                decoded[i] = (char)t;
            }

            for (int i = 0; i < s.Length; i++)
                result = result + decoded[i];

            return result;
        }
        // дешифрование по цезарю

        static async Task Main(string[] args)
        {
            //Console.WriteLine(GenPassword());

            // ввод текста 
            //Console.WriteLine("Enter text:");
            //string text = Console.ReadLine();

            // ввод сдвига
            //Console.Write("Enter shift:");
            //int sh = int.Parse(Console.ReadLine());

            // шифровка 
            //string encoded = Encoded(text, sh);
            // дешифровка
            //string decoded = Decoded(encoded, sh);

            // вывод
            //Console.WriteLine(encoded);
            //Console.WriteLine(decoded);



            string path = "note1.txt";
            // асинхронное чтение
            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();
                Console.WriteLine(text);
            }



            string path = "C:/Users/user/Desktop/HEI/Informatics/C#/Laba4/encoded.txt";
            string text = "Hello World\nHello METANIT.COM";

            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(path, false))
                await writer.WriteLineAsync(text);

            Console.ReadKey();
        }
    }
}
