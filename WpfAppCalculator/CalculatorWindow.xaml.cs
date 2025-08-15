using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        private string currentInput = "0";
        private string previousExpression = "";
        private double result = 0;
        private char? operation = null;
        
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string number = button.Content.ToString();

            if (number == "." && currentInput.Contains("."))
                return;

            if (currentInput == "0" && number != ".")
                currentInput = "";
            
            currentInput += number;
            UpdateDisplay();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(currentInput, out double num))
                return;

            if (operation != null)
                Calculate();
            else
                result = num;

            Button button = sender as Button;
            operation = button.Content.ToString()[0];
            previousExpression = result + " " + operation;
            currentInput = "0";
            UpdateDisplay();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
            operation = null;
            previousExpression = "";
            UpdateDisplay();
        }

        private void Calculate()
        {
            if (operation == null || !double.TryParse(currentInput, out double num))
                return;

            switch (operation)
            {
                case '+': result += num; break;
                case '-': result -= num; break;
                case '*': result *= num; break;
                case '/':
                    if (num == 0) { MessageBox.Show("Деление на ноль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                    result /= num;
                    break;
            }

            currentInput = result.ToString();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            UpdateDisplay();
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            previousExpression = "";
            result = 0;
            operation = null;
            UpdateDisplay();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput == "0") 
                return;
            if (currentInput.Length > 0)
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            PreviousOperations.Text = previousExpression;
            CurrentNumber.Text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
        }
    }
}