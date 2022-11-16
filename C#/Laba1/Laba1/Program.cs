using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    internal class Program
    {
        // functions Task 1
        static int[] sbyte_to_int_arr_bin(sbyte a)
        // функция перевода sbyte в массив интов 
        {
            sbyte a_2 = 1;
            int[] arr_bin = new int[8];
            for (int i = 0; i < arr_bin.Length; i++)
            {
                if ((a & a_2) != 0)
                {
                    arr_bin[i] = 1;
                }
                a_2 = (sbyte)(a_2 << 1);
            }
            return arr_bin;
        }

        static string int_arr_bin_to_str(int[] arr)
        // функция перевода масисива интов в строку
        {
            string result = "";
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                result += arr[i].ToString();
            }
            return result;
        }


        // functions Task 2
        static int[] str_to_int_arr_bin(string str_bin_number)
        // функция перевода строки в массив интов
        {
            int[] result = new int[8];
            int len_str = str_bin_number.Length;
            if (len_str < 8)
            {
                int dif = 8 - len_str;
                for (int i = 0; i < dif; i++)
                {
                    result[i] = 0;
                }
                for (int i = 0; i < len_str; i++)
                {
                    if (str_bin_number[i] == '0')
                    {
                        result[i + dif] = 0;
                    }
                    else if (str_bin_number[i] == '1')
                    {
                        result[i + dif] = 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (str_bin_number[i] == '0')
                    {
                        result[i] = 0;
                    }
                    else if (str_bin_number[i] == '1')
                    {
                        result[i] = 1;
                    }
                }
            }

            return result;
        }
        static sbyte int_arr_bin_to_sbyte(int[] arr_bin)
        // функция перевода массива интов в sbyte
        {
            int check = 0;
            sbyte result = 0, one = 1;

            for (int i = 0; i < arr_bin.Length; i++)
            {
                if (arr_bin[i] == 1)
                {
                    result = (sbyte)(result << 1);
                    result = (sbyte)(result | one);
                }
                else
                {
                    result = (sbyte)(result << 1);
                }

                if (arr_bin[i] == 1)
                {
                    check++;
                }
            }
            if (check == 0)
            {
                result = 0;
                return result;
            }
            else
            {
                return result;
            }

        }


        // functions Task 3
        static int[] revers_arr(int[] arr_orig)
        // функция разворота массива  
        {
            int[] arr_revers = new int[arr_orig.Length];
            for (int i = 0; i < arr_orig.Length; i++)
            {
                arr_revers[i] = arr_orig[arr_orig.Length - i - 1];
            }
            return arr_revers;
        }
        static int[] invers_arr(int[] arr_orig)
        // функция инверсии массива
        {
            int[] arr_invers = new int[arr_orig.Length];
            for (int i = 0; i < arr_orig.Length; i++)
            {
                if (arr_orig[i] == 0)
                {
                    arr_invers[i] = 1;
                }
                else
                {
                    arr_invers[i] = 0;
                }
            }
            return arr_invers;
        }
        static int[] plus_one_to_int_arr_bin(int[] arr_orig)
        // функция прибавления 1 к двоичному представлению числа 
        {
            for (int i = arr_orig.Length - 1; i >= 0; i--)
            {
                if (arr_orig[i] == 0)
                {
                    arr_orig[i] = 1;
                    break;
                }
                else
                {
                    arr_orig[i] = 0;
                }
            }

            return arr_orig;
        }
        static int[] int_to_int_arr_bin(int number)
        // функция перевода переменной инта в массив интов  
        {
            if (number > 0)
            {
                int[] result = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    result[i] = number % 2;
                    number /= 2;
                }
                return revers_arr(result);
            }
            else if (number < 0)
            {
                number = -number;
                int[] result = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    result[i] = number % 2;
                    number /= 2;
                }

                result = revers_arr(result);
                result = invers_arr(result);
                result = plus_one_to_int_arr_bin(result);

                return result;
            }
            else
            {
                int[] result2 = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                return result2;
            }

        }


        // functions Task 4
        static int int_arr_bin_to_int(int[] arr_orig)
        // функция перевода массива интов в переменную инт
        {
            int result = 0;
            if (arr_orig[0] == 1)
            {
                arr_orig = invers_arr(arr_orig);
                arr_orig = plus_one_to_int_arr_bin(arr_orig);
                arr_orig = revers_arr(arr_orig);
                for (int i = 0; i < arr_orig.Length; i++)
                {
                    int deg = Convert.ToInt32(Math.Pow(2, i));
                    result += deg * arr_orig[i];
                }
                return -result;
            }
            else
            {
                arr_orig = revers_arr(arr_orig);
                for (int i = 0; i < arr_orig.Length; i++)
                {
                    int deg = Convert.ToInt32(Math.Pow(2, i));
                    result += deg * arr_orig[i];
                }
                return result;
            }
        }


        // convenience functions
        static void ShowArr(int[] arr)
        // функция вывода массива
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
        }

        static void Main(string[] args)
        {
            // Test
            ///*

            ///*



            // Task 1
            ///* 
            Console.WriteLine("Задание 1");
            Console.Write("Введите число в диапазоне -128 до 127: ");
            string str_number = Console.ReadLine();
            int int_number = int.Parse(str_number);
            if (-128 <= int_number && int_number <= 127)
            {
                sbyte num = (sbyte)int_number;
                Console.WriteLine($"Число в двоичном коде: {int_arr_bin_to_str(sbyte_to_int_arr_bin(num))}");
            }
            else
            {
                Console.WriteLine("Число не входит в диапазон");
            }
            Console.ReadKey();
            //*/



            // Task 2
            ///*
            Console.WriteLine("Задание 2");
            Console.Write("Введите двоичную запись числа не более 8 символов: ");
            //string str_number = Console.ReadLine();
            str_number = Console.ReadLine();
            if ( 1 <= str_number.Length && str_number.Length <= 8 )
            {
                Console.WriteLine($"Число в десятичном виде: {int_arr_bin_to_sbyte(str_to_int_arr_bin(str_number))}");
            }
            else if (str_number.Length == 0)
            {
                Console.WriteLine("Вы ввели пустую строку");
            }
            else
            {
                Console.WriteLine("Введите число короче");
            }
            Console.ReadKey();
            //*/



            // Task 3 
            ///*
            Console.WriteLine("Задание 3");
            Console.Write("Введите число в диапазоне -128 до 127: ");
            //int int_number = Console.ReadLine();
            int_number = int.Parse(Console.ReadLine());
            if (-128 <= int_number && int_number <= 127)
            {
                Console.Write("Число в двоичном виде: ");
                ShowArr(int_to_int_arr_bin(int_number));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Введите цисло в диапазоне");
            }
            Console.ReadKey();
            //*/



            // Task 4
            ///*
            Console.WriteLine("Задание 4");
            Console.Write("Введите двоичное представление числа: ");
            //string str_number = Console.ReadLine();
            str_number = Console.ReadLine();

            Console.WriteLine($"Число в десятичном виде: {int_arr_bin_to_int(str_to_int_arr_bin(str_number))}");

            Console.ReadKey();
            //*/
        }
    }
}