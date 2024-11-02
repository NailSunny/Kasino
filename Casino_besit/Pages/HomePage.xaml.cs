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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string _password;
        public HomePage(string password)
        {

            InitializeComponent();
            _password = password;
        }


        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).FirstOrDefault();
            NavigationService.Navigate(new ReportPage(user.Password));
        }

        private void Btn_Finance_Click(object sender, RoutedEventArgs e)
        {
            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).AsEnumerable().FirstOrDefault();
            if (user != null)
            {
                NavigationService.Navigate(new BalancePage(user.Password));
            }

        }

        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {
            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).AsEnumerable().FirstOrDefault();
            if (user != null)
            {
               NavigationService.Navigate(new GameHistoryPage(user.Password));
            }

        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());

        }

        private void Btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).AsEnumerable().FirstOrDefault();
            if (user != null)
            {
                UserProfileWindow newWindow = new UserProfileWindow(user.Password);
                newWindow.Show();
            }
        }

        private void Play_Bandit(object sender, RoutedEventArgs e)
        {

            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).AsEnumerable().FirstOrDefault();
            
            // Передаем текущего пользователя и текущую игру
            NavigationService.Navigate(new BanditPage(user.Password));
        }

        private void Play_ruletka_Click(object sender, RoutedEventArgs e)
        {
            var user = ConnectionClass.DB.Users.Where(z => z.Password == _password).AsEnumerable().FirstOrDefault();

            // Передаем текущего пользователя и текущую игру
            NavigationService.Navigate(new RuletkaPage(user.Password));
        }
    }
}
