using System.Windows;
using System.Windows.Controls;

namespace CalcPKTBA
{
    public partial class MainWindow : Window
    {
        string leftop = "";    // Левый операнд
        string operation = ""; // Знак операции
        string rightop = "";   // Правый операнд

        History historyWindow = new History();

        public MainWindow()
        {
            InitializeComponent();
            // Добавляем обработчик для всех кнопок на гриде
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button button)
                {
                    button.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;

            if(textBlock.Text.Contains("="))
            {
                leftop = "";
                textBlock.Text = "";
            }

            if (s != "История")
            {
                // Добавляем его в текстовое поле
                textBlock.Text += s;

                //Не позволяет размещать одинаковые символы
                if (textBlock.Text.Contains("--") || textBlock.Text.Contains("++") || textBlock.Text.Contains("//") || textBlock.Text.Contains("**"))
                {
                    textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1);
                }
            }

            // Пытаемся преобразовать в число
            bool result = long.TryParse(s, out _);

            // Если текст - это число
            if (result)
            {
                // Если операция не задана
                if (operation == "")
                {
                    // Добавляем к левому операнду
                    leftop += s;
                }
                else
                {
                    // Иначе к правому операнду
                    rightop += s;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (s == "=")
                {
                    Update_RightOp();
                    textBlock.Text += rightop;
                    operation = "";
                    rightop = "";

                    historyWindow.calculationsHistory = textBlock.Text;
                    historyWindow.getHistory();
                }
                // Очищаем поле и переменные
                else if (s == "Очистить")
                {
                    leftop = "";
                    rightop = "";
                    operation = "";
                    textBlock.Text = "";
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому операнду, а правый операнд очищаем
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        // Обновляем значение правого операнда
        private void Update_RightOp()
        {
            long num1, num2;

            if (leftop != string.Empty && long.TryParse(leftop, out _))
            {
                if (!long.TryParse(leftop, out _))
                {
                    leftop = "Большое число";
                    return;
                }
                num1 = long.Parse(leftop);
            }
            else
            {
                return;
            }

            if (rightop != string.Empty)
            {
                if (!long.TryParse(rightop, out _))
                {
                    rightop = "Большое число";
                    return;
                }
                num2 = long.Parse(rightop);
            }
            else
            {
                num2 = num1;
                rightop = num1.ToString();
            }

            // Выполняем операцию
            switch (operation)
            {
                case "+":
                    rightop = (num1 + num2).ToString();
                    break;
                case "-":
                    rightop = (num1 - num2).ToString();
                    break;
                case "*":
                    rightop = (num1 * num2).ToString();
                    break;
                case "/":
                    rightop = num2 != 0 ? (num1 / num2).ToString() : "Невозможная операция";
                    break;
            }
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // MainWindow главное окно для taskWindow
            historyWindow.Owner = this;
            historyWindow.Show();
        }
    }
}
