using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba2_with_interface_
{
    public partial class MainWindow : Window
    {
        int[] A = new int[8];
        int[] B = new int[8];
        // массивы для работы

        public MainWindow()
        {
            InitializeComponent();
            
            for (int i = 0; i < 8; i++)
            {

                Button btnA = new Button();
            
                btnA.Tag = i;
                btnA.Content = 0;
                
                btnA.Width = 30;
                btnA.Height = 30;
                btnA.Background = new SolidColorBrush();
                btnA.Foreground = new SolidColorBrush(Colors.White);
                btnA.FontSize = 16;

                btnA.Click += Button_Click_A;


                Button btnB = new Button();

                btnB.Tag = i;
                btnB.Content = 0;
                   
                btnB.Width = 30;
                btnB.Height = 30;
                btnB.Background = new SolidColorBrush();
                btnB.Foreground = new SolidColorBrush(Colors.White);
                btnB.FontSize = 16;

                btnB.Click += Button_Click_B;



                ugrA.Children.Add(btnA);
                ugrB.Children.Add(btnB);

            }
        
        }
        // создание 16 кнопок = 2 двоичных пердставлений
        // в гридах

        private void Button_Click_A(object sender, RoutedEventArgs e)
        {
            int id = (int)(((Button)sender).Tag);

            if (A[id] == 1)
                A[id] = 0;
            else
                A[id] = 1;

            ((Button)sender).Content = A[id];

            resA.Content = BinToDec(A);
        }
        // реакция кнопок первого массива 

        private void Button_Click_B(object sender, RoutedEventArgs e)
        {
            int id = (int)(((Button)sender).Tag);

            if (B[id] == 1)
                B[id] = 0;
            else
                B[id] = 1;

            ((Button)sender).Content = B[id];

            resB.Content = BinToDec(B);
        }
        // реакция кнопок второго массива

        sbyte BinToDec(int[] n)
        {
            sbyte res = 0;

            for (int i = 0; i < 8; i++)
            {
                res += (sbyte)((1 << i) * n[7 - i]);
            }

            return res;
        }
        // функция перевода двоичного представления в десятичное

        int[] add_one(int[] n, int start)
        {
            int k = 1;

            for (int i = start; i >= 0; i--)
            {
                n[i] += k;
                k = 0;

                if (n[i] > 1)
                {
                    k = 1;
                    n[i] = 0;
                }
                if (k == 0)
                    break;
            }

            return n;
        }
        // функция добавления единицы к двоичному представлению

        string int_arr_bin_to_str(int[] arr)
        // функция перевода масисива интов в строку
        {
            string result = "";
            for (int i =  0; i < 8; i++)
            {
                result += arr[i].ToString();
            }
            return result;
        }

        int[] AddBin(int[] n1, int[] n2)
        {
            int[] n3 = new int[8];

            n1.CopyTo(n3, 0);

            for (int i = 7; i >= 0; i--)
            {
                if (n2[i] == 1)
                    n3 = add_one(n3, i);
            }

            return n3;
        }
        // функция сложения двоичных представлений 

        int[] invers_arr(int[] arr_orig)
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

        private void Button_Click_Pluse(object sender, RoutedEventArgs e)
        {
            int[] C = AddBin(A, B);
            resSum_Bin.Content = int_arr_bin_to_str(C);
            resSum_Dec.Content = BinToDec(C);
        }
        // функция реакции кнопки "+"

        int[] SubBin(int[] n1, int[] n2)
        {
            int[] n3 = new int[8];
            int[] n4 = new int[8];

            n1.CopyTo(n3, 0);
            n2.CopyTo(n4, 0);

            n4 = invers_arr(n4);
            n4 = add_one(n4, 7);

            for (int i = 7; i >= 0; i--)
            {
                if (n4[i] == 1)
                    n3 = add_one(n3, i);
            }

            return n3;
        }
        // функция вычитания двоичных представлений 

        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            int[] C = SubBin(A, B);
            resSub_Bin.Content = int_arr_bin_to_str(C);
            resSub_Dec.Content = BinToDec(C);
        }
        // функция реакции кнопки "-"

        int[] MulBin(int[] n1, int[] n2)
        {
            int[] n3 = new int[8];
            int[] r = new int[8];

            for (int i = 7; i >= 0; i--)
            {
                n3 = LeftShift(n1, 7 - i);
                if (n2[i] == 1)
                    r = AddBin(r, n3);
            }   

            return r;
        }
        // функция умножения двоичных представлений 

        private void Button_Click_Star(object sender, RoutedEventArgs e)
        {
            int[] C = MulBin(A, B);
            resMul_Bin.Content = int_arr_bin_to_str(C);
            resMul_Dec.Content = BinToDec(C);
        }
        // функция реакции кнопки "*"

        int[] LeftShift(int[] n, int sh)
        {
            if (sh == 0) return n;

            int[] copy = new int[8];
            n.CopyTo(copy, 0);

            int i, j;
            for (i = 0, j = sh; j < 8; i++, j++)
            {
                copy[i] = copy[j];
                copy[j] = 0;  
            }

            for (; i < 8; i++)
                copy[i] = 0;

            return copy;
        }
        // функция сдвига 

        int[] DivBin(int[] n1, int[] n2)
        {
            int[] pr = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] r = { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (n1[0] == 0 && n2[0] == 0) // если оба положительны
            {
                for (int i = 0; i < 8; i++)
                {
                    pr = LeftShift(pr, 1);
                    pr[7] = n1[i];

                    int[] c = SubBin(pr, n2);

                    if (c[0] == 0)
                    {
                        r = LeftShift(r, 1);
                        r = add_one(r, 7);
                        c.CopyTo(pr, 0);
                    }
                    else
                        r = LeftShift(r, 1);
                }

                return r;
            }
            else if (n1[0] == 1 && n2[0] == 0) // если первое оторицательно 
            {
                int[] n1_copy_p = new int[8];
                n1.CopyTo(n1_copy_p, 0);
                n1_copy_p = invers_arr(n1_copy_p);
                n1_copy_p = add_one(n1_copy_p, 7);
                // теперь оба положительны

                for (int i = 0; i < 8; i++)
                {
                    pr = LeftShift(pr, 1);
                    pr[7] = n1_copy_p[i];

                    int[] c = SubBin(pr, n2);

                    if (c[0] == 0)
                    {
                        r = LeftShift(r, 1);
                        r = add_one(r, 7);
                        c.CopyTo(pr, 0);
                    }
                    else
                        r = LeftShift(r, 1);
                }

                r = invers_arr(r);
                r = add_one(r, 7);
                return r;
            }
            else if (n1[0] == 0 && n2[0] == 1) // если второе отрицательно
            {
                int[] n2_copy_p = new int[8];
                n2.CopyTo(n2_copy_p, 0);
                n2_copy_p = invers_arr(n2_copy_p);
                n2_copy_p = add_one(n2_copy_p, 7);
                // теперь второе трицательно 

                for (int i = 0; i < 8; i++)
                {
                    pr = LeftShift(pr, 1);
                    pr[7] = n1[i];

                    int[] c = SubBin(pr, n2_copy_p);

                    if (c[0] == 0)
                    {
                        r = LeftShift(r, 1);
                        r = add_one(r, 7);
                        c.CopyTo(pr, 0);
                    }
                    else
                        r = LeftShift(r, 1);
                }

                r = invers_arr(r);
                r = add_one(r, 7);
                return r;
            }
            else // если оба отрицательны
            {
                int[] n1_copy_p = new int[8];
                n1.CopyTo(n1_copy_p, 0);
                n1_copy_p = invers_arr(n1_copy_p);
                n1_copy_p = add_one(n1_copy_p, 7);

                int[] n2_copy_p = new int[8];
                n2.CopyTo(n2_copy_p, 0);
                n2_copy_p = invers_arr(n2_copy_p);
                n2_copy_p = add_one(n2_copy_p, 7);

                // теперь оба положительны

                for (int i = 0; i < 8; i++)
                {
                    pr = LeftShift(pr, 1);
                    pr[7] = n1_copy_p[i];

                    int[] c = SubBin(pr, n2_copy_p);

                    if (c[0] == 0)
                    {
                        r = LeftShift(r, 1);
                        r = add_one(r, 7);
                        c.CopyTo(pr, 0);
                    }
                    else
                        r = LeftShift(r, 1);
                }

                return r;
            }
        }
        // функция деления двоичных представлений 

        private void Button_Click_Slesh(object sender, RoutedEventArgs e)
        {
            int[] C = DivBin(A, B);
            resDiv_Bin.Content = int_arr_bin_to_str(C);
            resDiv_Dec.Content = BinToDec(C);
        }
        // функция реакции кнопки "/"

    }
}
