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
    /// Логика взаимодействия для GameSessionPage.xaml
    /// </summary>
    public partial class GameSessionPage : Page
    {
        public GameSessionPage()
        {
            InitializeComponent();
            SessionsListView.ItemsSource = ConnectionClass.DB.Game_Sessions.ToList();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void Button_Users_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }
    }
}

