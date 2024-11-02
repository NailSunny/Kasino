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
    /// Логика взаимодействия для GameHistoryPage.xaml
    /// </summary>
    public partial class GameHistoryPage : Page
    {
        string _password;
        public GameHistoryPage(string password)
        {
            _password = password;
            InitializeComponent();
            var user = ConnectionClass.DB.Users.FirstOrDefault(x => x.Password == password);
            HistoryListView.ItemsSource = ConnectionClass.DB.Game_History.Where(a => a.User_ID == user.User_ID).ToList();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}
