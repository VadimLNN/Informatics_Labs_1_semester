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

namespace Laba4_with_intarface_
{
    public partial class MainWindow : Window
    {
        string path = "", text = "", encoded = "", decoded = "";
        int sh = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_read_file_Click(object sender, RoutedEventArgs e)
        {
            path = txbx_path.Text + "\\orig.txt";

            using (StreamReader reader = new StreamReader(path))
                text = await reader.ReadToEndAsync();

            txbx_file_content.Text = text;
        }

        string Encoded(string s, int sh)
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
        
        private async void btn_encoded_ClickAsync(object sender, RoutedEventArgs e)
        {
            path = txbx_path.Text + "\\orig.txt";
            sh = int.Parse(s: txbx_shift.Text);

            encoded = Encoded(text, sh);
            txbx_file_content.Text = encoded;

            path = txbx_path.Text + "\\encoded.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
                await writer.WriteLineAsync(encoded);
        }

        private async void btn_decoded_ClickAsync(object sender, RoutedEventArgs e)
        {
            path = txbx_path.Text + "\\encoded.txt";
            sh = int.Parse(s: txbx_shift.Text);

            decoded = Decoded(encoded, sh);
            txbx_file_content.Text = decoded;

            path = txbx_path.Text + "\\decoded.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
                await writer.WriteLineAsync(encoded);

        }
    }
}
