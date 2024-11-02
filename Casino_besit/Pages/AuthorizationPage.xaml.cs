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
using Wpf.Ui.Appearance;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {

        public Users user;
        public Games game;
        public AuthorizationPage()
        {
            InitializeComponent();
        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PassBox.Password;


            var loginObj = ConnectionClass.DB.Users.FirstOrDefault(log => log.Login == login && log.Password == password);
          

            if (loginObj == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            else if (loginObj != null)
            {
                if (loginObj.RoleID == 2)
                {
                    
                    NavigationService.Navigate(new HomePage(password));
                }
                else if (loginObj.RoleID == 1)
                {
                    
                    NavigationService.Navigate(new GameSessionPage());
                }
            }
            LoginTextBox.Text = "";
            PassBox.Password = "";
            MainWindow.user = loginObj;
        }
    }
}

