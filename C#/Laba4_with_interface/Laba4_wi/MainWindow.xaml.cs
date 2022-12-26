using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Laba4_wi
{
    public partial class MainWindow : Window
    {
        string path = "", text = "", encoded = "", decoded = "";
        int sh = 0, len = 0, start = 0, end = 0;
        Random rng = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_read_file_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader reader = new StreamReader(path))
                text = await reader.ReadToEndAsync();

            txbx_file_content.Text = text;
        }
        // считать контент файла 

        string Encoded(string s, int sh)
        {
            string result = "";

            char[] encoded = new char[s.Length];        // массив символов зашифрованного текста 

            for (int i = 0; i < s.Length; i++)
                encoded[i] = (char)((((byte)s[i]) + sh) % 1112064); // преобразование ихначального символа в число == его порядок
                                                                    // в таблице в пределах 1112064

            for (int i = 0; i < s.Length; i++)          // создание зашифрованной строки из массива чаров 
                result = result + encoded[i];

            return result;
        }
        // шифрование по цезарю
        
        private void Button_Click_Set_File_Path(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            txbx_path.Text = dlg.FileName;
            path = dlg.FileName;
        }
        // выбор файла

        static string Decoded(string s, int sh)
        {
            string result = "";

            char[] decoded = new char[s.Length];

            for (int i = 0; i < s.Length; i++)      // заполнение массива чаров 
            {
                int t = (int)((byte)(s[i] - sh));   // перевод в порядок символа таблицы - сдвиг

                if (t < 0) t += 1112064;            // проверка на отрицательные значения 

                decoded[i] = (char)t;               // перевод в символ 
            }

            for (int i = 0; i < s.Length; i++)      // создание строки из чаров 
                result = result + decoded[i];

            return result;
        }
        // дешифрование по цезарю

        private async void btn_encoded_ClickAsync(object sender, RoutedEventArgs e)
        {
            sh = int.Parse(s: txbx_shift.Text);

            encoded = Encoded(text, sh);
            txbx_file_content.Text = encoded;

            using (StreamWriter writer = new StreamWriter(path, false))
                await writer.WriteLineAsync(encoded);
        }
        // закодировать 

        private async void btn_decoded_ClickAsync(object sender, RoutedEventArgs e)
        {
            sh = int.Parse(s: txbx_shift.Text);

            decoded = Decoded(encoded, sh);
            txbx_file_content.Text = decoded;

            using (StreamWriter writer = new StreamWriter(path, false))
                await writer.WriteLineAsync(decoded);

        }
        // декодировать

        private void btn_create_password(object sender, RoutedEventArgs e)
        {
            len = int.Parse(s: txbx_lenght.Text);
            start = int.Parse(s: txbx_start.Text, System.Globalization.NumberStyles.HexNumber);
            end = int.Parse(s: txbx_end.Text, System.Globalization.NumberStyles.HexNumber);

            string password = "";

            for (int i = 0; i < len; i++)
            {
                char ch = (char)rng.Next(start, end);
                password += ch;
            }

            txbx_password.Text = password;
        }
        // создать пароль
    }
}