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
using Casino_besit.DataBase;
using Casino_besit.Pages;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            UsersListView.ItemsSource = ConnectionClass.DB.Users.ToList();
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameSessionPage());
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersListView.SelectedItem as Users;
            if (selectedUser != null)
            {
                DeleteUserInfo(selectedUser);
                ConnectionClass.DB.Users.Remove(selectedUser);
                ConnectionClass.DB.SaveChanges();
            }
            UsersListView.ItemsSource = ConnectionClass.DB.Users.ToList();
        }

        private void UsersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = UsersListView.SelectedItem as Users;
            if (a != null)
            {
                UserProfileWindow newWindow = new UserProfileWindow(a.Password);
                newWindow.Show();
            }
        }
        private void DeleteUserInfo(Users user)
        {
            var transactions = ConnectionClass.DB.Transactions.Where(t => t.User_ID == user.User_ID);
            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    ConnectionClass.DB.Transactions.Remove(transaction);
                }
            }

            var sessions = ConnectionClass.DB.Game_Sessions.Where(s => s.User_ID == user.User_ID);
            if (sessions != null)
            {
                foreach (var session in sessions)
                {
                    ConnectionClass.DB.Game_Sessions.Remove(session);
                }
            }

            var histories = ConnectionClass.DB.Game_History.Where(h => h.User_ID == user.User_ID);
            if (histories != null)
            {
                foreach (var history in histories)
                {
                    ConnectionClass.DB.Game_History.Remove(history);
                }
            }

            ConnectionClass.DB.SaveChanges();
        }
    }
}
