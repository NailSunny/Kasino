using Casino_besit.DataBase;
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
    /// Логика взаимодействия для WithdrawWindow.xaml
    /// </summary>
    public partial class WithdrawWindow : Window
    {
        public int Amount { get; private set; }
        public string Method { get; private set; }

        public WithdrawWindow()
        {
            InitializeComponent();
        }

        private void ButtonWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxWithdrawMethod.SelectedItem?.ToString()))
            {
                MessageBox.Show("Пожалуйста, выберите способ пополнения и введите сумму.");
                return;
            }
            if (string.IsNullOrEmpty(textBoxWithdrawAmount.Text) || !int.TryParse(textBoxWithdrawAmount.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму для вывода!");
                return;
            }
            Amount = amount;
            Method = (comboBoxWithdrawMethod.SelectedItem as ComboBoxItem)?.Content.ToString();
            DialogResult = true; // Устанавливаем результат диалога
            Close(); // Закрываем окно
        }

        
    }
}
