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

            char[] encoded = new char[s.Length];        // массив символов зашифрованного текста 

            for (int i = 0; i < s.Length; i++)
                encoded[i] = (char)((((byte)s[i]) + sh) % 256); // преобразование ихначального символа в число == его порядок
                                                                // в таблице в пределах 256 

            for (int i = 0; i < s.Length; i++)          // создание зашифрованной строки из массива чаров 
                result = result + encoded[i];

            return result;
        }
        // шифрование по цезарю

        static string Decoded(string s, int sh)
        {
            string result = "";

            char[] decoded = new char[s.Length];

            for (int i = 0; i < s.Length; i++)      // заполнение массива чаров 
            {
                int t = (int)((byte)(s[i] - sh));   // перевод в порядок символа таблицы - сдвиг

                if (t < 0) t += 255;                // проверка на отрицательные значения 

                decoded[i] = (char)t;               // перевод в символ 
            }

            for (int i = 0; i < s.Length; i++)      // создание строки из чаров 
                result = result + decoded[i];

            return result;
        }
        // дешифрование по цезарю

        static async Task Main(string[] args)
        {
            string path_orig = "C:/Users/user/Desktop/HEI/Informatics/C#/Laba4/orig.txt";
            string path_encoded = "C:/Users/user/Desktop/HEI/Informatics/C#/Laba4/encoded.txt";
            string path_decoded = "C:/Users/user/Desktop/HEI/Informatics/C#/Laba4/decoded.txt";
            string text = "";

            //Console.WriteLine(GenPassword());




            // ввод текста из файла оргинала
            using (StreamReader reader = new StreamReader(path_orig))
                text = await reader.ReadToEndAsync();
            Console.WriteLine(text);
            
            // ввод сдвига
            Console.Write("Enter shift:");
            int sh = int.Parse(Console.ReadLine());


            // шифровка и запись в файл 
            string encoded = Encoded(text, sh);
            using (StreamWriter writer = new StreamWriter(path_encoded, false))
                await writer.WriteLineAsync(encoded);
            Console.WriteLine(encoded);


            // дешифровка и запись в файл
            string decoded = Decoded(encoded, sh);
            using (StreamWriter writer = new StreamWriter(path_decoded, false))
                await writer.WriteLineAsync(decoded);
            Console.WriteLine(decoded);


            Console.ReadKey();
        }
    }
}
