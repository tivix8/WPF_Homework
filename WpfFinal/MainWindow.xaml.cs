using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfFinal {
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Recalculate(object sender, TextChangedEventArgs e)
        {
            if (BillTextBox == null ||
                TipPercentTextBox == null ||
                PeopleTextBox == null ||
                TipResultText == null ||
                TotalResultText == null ||
                PerPersonResultText == null)
                return;

            double bill = ToDouble(BillTextBox.Text);
            double percent = ToDouble(TipPercentTextBox.Text);
            int people = ToInt(PeopleTextBox.Text);

            if (people <= 0) people = 1;

            double tip = bill * percent / 100;
            double total = bill + tip;
            double perPerson = total / people;

            TipResultText.Text = tip.ToString("0.00");
            TotalResultText.Text = total.ToString("0.00");
            PerPersonResultText.Text = perPerson.ToString("0.00");
        }


        private void TipButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                TipPercentTextBox.Text = btn.Content.ToString().Replace("%", "");
            }
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            BillTextBox.Text = "";
            TipPercentTextBox.Text = "15";
            PeopleTextBox.Text = "1";

            TipResultText.Text = "0.00";
            TotalResultText.Text = "0.00";
            PerPersonResultText.Text = "0.00";
        }


        private void OnlyDigits(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }


        private double ToDouble(string text)
        {
            double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value);
            return value;
        }

        private int ToInt(string text)
        {
            int.TryParse(text, out int value);
            return value;
        }
    }
}