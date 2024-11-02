using Casino_besit.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для BalancePage.xaml
    /// </summary>
    public partial class BalancePage : Page
    {
        private string _password;

        public BalancePage(string password)
        {
            InitializeComponent();
            this.DataContext = MainWindow.user;
            _password = password;
            Refresh();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OpenDepositWindow_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow depositWindow = new DepositWindow();
            if (depositWindow.ShowDialog() == true) // Если диалог был успешным
            {
                ProcessDeposit(depositWindow.Amount, depositWindow.Method);
            }
        }

        private void OpenWithdrawWindow_Click(object sender, RoutedEventArgs e)
        {
            WithdrawWindow withdrawWindow = new WithdrawWindow();
            if (withdrawWindow.ShowDialog() == true) // Если диалог был успешным
            {
                ProcessWithdraw(withdrawWindow.Amount, withdrawWindow.Method);
            }
        }

        private void ProcessDeposit(int amount, string method)
        {
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);

            if (user != null)
            {
                Transactions transaction = new Transactions
                {
                    User_ID = user.User_ID,
                    Amount = amount,
                    Transaction_Type = "Пополнение",
                    Transaction_Date = DateTime.Now,
                    Sposob = method
                };

                user.Balance += amount; // Обновление баланса
                ConnectionClass.DB.Transactions.Add(transaction);
                ConnectionClass.DB.SaveChanges();
                Refresh(); // Обновляем список транзакций
                UpdateBalanceTextBlock(Convert.ToInt32(user.Balance));
            }
        }

        private void ProcessWithdraw(int amount, string method)
        {
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (user != null && user.Balance >= amount) // Проверяем, достаточно ли средств
            {
                Transactions transaction = new Transactions
                {
                    User_ID = user.User_ID,
                    Amount = amount,
                    Transaction_Type = "Списание",
                    Transaction_Date = DateTime.Now,
                    Sposob = method
                };

                user.Balance -= amount; // Обновление баланса
                ConnectionClass.DB.Transactions.Add(transaction);
                ConnectionClass.DB.SaveChanges();
                Refresh(); // Обновляем список транзакций
                UpdateBalanceTextBlock(Convert.ToInt32(user.Balance));
            }
            else
            {
                MessageBox.Show("Недостаточно средств для вывода!");
            }
        }

        public void Refresh()
        {
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (user != null)
            {
                TransactionListView.ItemsSource = ConnectionClass.DB.Transactions.Where(t => t.User_ID == user.User_ID).ToList();
            }
            else
            {
                TransactionListView.ItemsSource = null;
            }
        }
        private void UpdateBalanceTextBlock(int newBalance)
        {
            BalanceText.Text = Convert.ToString(newBalance); // Обновляем текстовое поле с балансом
        }
    }
}
