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

namespace Laba3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //################################ TASKS 1-2 #####################
        byte[] ByteToBin(byte n)
        {
            byte[] r = new byte[8];

            for (int i = 0; i < r.Length; i++)
                r[7 - i] = (byte)((byte)(n >> i) & 1);
            
            return r;
        }
        // перевод байта в массив  

        string BinToString(byte[] arr)
        {
            string result = "";
            
            for (int i = 0; i < 8; i++)
                result += arr[i].ToString();
            
            return result;
        }
        // перевод двоичного представления байтов в строку 

        byte StringToByte(string s)
        {
            byte r = 0;

            for (int i = 0; i < 8; i++)
            {
                if (s[i] == '1')
                {
                    r = (byte)(r << 1);
                    r = (byte)(r | 1);
                }
                else
                    r = (byte)(r << 1);
            }

            return r;
        }
        // перевод подстроки из 8 символов в байт

        byte[] StrArrBinToArrByte(string s)
        {
            byte[] res = new byte[4];
            string _1 = "", _2 = "", _3 = "", _4 = "";

            for (int i = 0; i < 32; i++)
            {
                if (i / 8 == 0)
                {
                    _1 = _1 + s[i];
                }
                if (i / 8 == 1)
                {
                    _2 = _2 + s[i];
                }
                if (i / 8 == 2)
                {
                    _3 = _3 + s[i];
                }
                if (i / 8 == 3)
                {
                    _4 = _4 + s[i];
                }
            }

            res[0] = StringToByte(_4);
            res[1] = StringToByte(_3);
            res[2] = StringToByte(_2);
            res[3] = StringToByte(_1);

            return res;
        }
        // перевод строки в 4 подстроки и в масиив из 4 байтов

        private void btn_to_bin_Click(object sender, RoutedEventArgs e)
        {
            float n = float.Parse(txb_dec.Text);
            byte[] num = BitConverter.GetBytes(n);

            txb_bin.Text = "";
            txb_bin.Text += BinToString(ByteToBin(num[3]));
            txb_bin.Text += BinToString(ByteToBin(num[2]));
            txb_bin.Text += BinToString(ByteToBin(num[1]));
            txb_bin.Text += BinToString(ByteToBin(num[0]));
        }
        // реакция кнопки "To BIN"

        private void btn_to_dec_Click(object sender, RoutedEventArgs e)
        {
            string s = txb_bin.Text;                    // 32 бинарных знака 
            byte[] res_arr = StrArrBinToArrByte(s);     // 4 байта для конвертации через выстроенную функцию  

            float res = BitConverter.ToSingle(res_arr, 0);

            txb_dec.Text = $"{res}";
        }
        // реакция кнопки "To DEC"

        //################################ TASKS 3-4 #####################
        byte[] StringToArrByte(string s)
        {
            byte[] r = new byte[32];

            for (int i = 0; i < 32; i++)
                 r[i] = (byte)(s[i]-'0');

            return r;
        }
        // перевод строки в масиив байт 

        byte ArrBinToByte(byte[] n)
        {
            byte r = 0;

            for (int i = 7; i >= 0; i++)
                r |= (byte)(n[i] << (7 - i));

            return r;
        }
        // перевод массива байт в байт

        float BinToFloat(byte[] n, int p)
        {
            float r = 0;

            r += (float)(1 * Math.Pow(2, p));
            p--;

            for (int i = 9; i < 32; i++)
            { 
                r += (float)(n[i] * Math.Pow(2, p));
                p--;
            }

            if (n[0] == 1)
                r *= -1;

            return r;
        }
        // перевод массива байт в float

        string take_order (float b)
        {
            string result = "", buf = "";
            int por = 0; 

            if (b >= 1) // порядок числа с целой частью
            {
                byte a = (byte)Math.Floor(b);       // взятие целой части
                buf = BinToString(ByteToBin(a));    // перевод целой части в строку двоичного представления

                for (int i = 0; i < 8; i++)         // поиск единицы
                {
                    if (buf[i] == '1')
                    {
                        por = 7-i;                  // порядок 8 - положение первой единицы  
                        break;
                    }
                }
                a = (byte)(por+127);
                result = BinToString(ByteToBin(a));
            }
            else        // порядок числа без целой части
            {
                float bb = b;           
                for (int i = 0; i < 8; i++)         // создание 8 первых знаков двоичного представления дроби
                {
                    bb *= 2;
                    buf = buf + bb.ToString()[0];

                    if (bb >= 1)
                        bb--;
                }

                for (int i = 0; i < 8; i++)
                    if (buf[i] == '1')
                    {
                        por = i + 1;
                        break;
                    }
                        
                result = BinToString(ByteToBin((byte)(-por + 127)));
            }


            return result;
        }
        // взятие порядка числа 

        private void btn_to_bin_alg_Click(object sender, RoutedEventArgs e)
        {
            float n = float.Parse(txb_dec.Text);

            // определение знака 
            string zn = n < 0 ? "1" : "0";
            if (zn == "1")
                n *= -1;

            string serial = "", buf = "", dec_part = "", float_part = "";  

            if (n >= 1)
            {
                byte a = (byte)((n * 10) / 10);     // взятие целой части
                buf = BinToString(ByteToBin(a));    // перевод целой части в строку двоичного представления 
                bool flag = false;

                for (int i = 0; i < 8; i++)
                {
                    if (flag)
                        dec_part = dec_part + buf[i];   // запись десятичной части без первого знака 

                    if (buf[i] == '1')                   
                        flag = true;
                }
            }

            if (n % 1 != 0)
            {
                int check = 0;
                float v = n % 1;                                    // взятие десятичной части
                for (int i = 0; i < (23 - dec_part.Length); i++)
                {
                    v *= 2;

                    float_part = float_part + (v.ToString())[0];

                    if (v >= 1)
                        v--;
                }
            }
            else
            {
                for (int i = 0; i < (23 - dec_part.Length); i++)
                    float_part = float_part + '0';
            }



            // определение мантисы
            string mantisa = dec_part + float_part;
            
            // определение порядка 
            serial = take_order(n);


            txb_bin.Text = $"{zn}{serial}{mantisa}";
        }
        // реакция кнопки "To BIN alg"


        private void btn_to_dec_alg_Click(object sender, RoutedEventArgs e)
        {
            byte[] num = StringToArrByte(txb_bin.Text);
            byte[] serial = new byte[8];

            for (int i = 0, j = 1; i < 8; i++, j++)
                serial[i] = num[j];
            
            byte pow = ArrBinToByte(serial);
            pow -= 127;


            txb_dec.Text = (BinToFloat(num, pow)).ToString();
        }
        // реакция кнопки "To DEC alg"
    }
} 
