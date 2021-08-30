using System;
using System.Text;
using System.Windows;
using System.IO;
using System.ComponentModel;

namespace CalcPKTBA
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public string calculationsHistory;
        private readonly string path = "C:\\calcHistory.txt";

        public History()
        {
            InitializeComponent();
            readFromTextFile();
        }
        public void getHistory()
        {
            historyBlock.Text += calculationsHistory + "\n";
            writeToTextFile();
        }

        public void writeToTextFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8); // Логика записи в файл: записывается все что в этом окне
                sw.Write(historyBlock.Text);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Выполнение блока finally.");
            }
        }

        public void readFromTextFile()
        {
            try
            {
                historyBlock.Text = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Выполнение блока finally.");
            }
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                historyBlock.Text = string.Empty;
            }
            //try
            //{
            //    StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8); // Логика записи в файл: записывается все что в этом окне
            //    sw.Write("");
            //    sw.Close();
            //
            //    historyBlock.Text = string.Empty;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Ошибка: " + ex.Message);
            //}
            //finally
            //{
            //    Console.WriteLine("Выполнение блока finally.");
            //}
        }

        private void History_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }
    }
}
