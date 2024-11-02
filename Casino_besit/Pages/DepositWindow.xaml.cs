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
using System.Windows.Shapes;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для DepositWindow.xaml
    /// </summary>
    public partial class DepositWindow : Window
    {
        public int Amount { get; private set; }
        public string Method { get; private set; }

        public DepositWindow()
        {
            InitializeComponent();
        }

        private void ButtonDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxDepositMethod.SelectedItem?.ToString()))
            {
                MessageBox.Show("Пожалуйста, выберите способ пополнения и введите сумму.");
                return;
            }
            if (string.IsNullOrEmpty(textBoxDepositAmount.Text) || !int.TryParse(textBoxDepositAmount.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму для пополнения!");
                return;
            }
            Amount = amount;
            Method = (comboBoxDepositMethod.SelectedItem as ComboBoxItem)?.Content.ToString();
            DialogResult = true; // Устанавливаем результат диалога
            Close(); // Закрываем окно
        }
    }
}
