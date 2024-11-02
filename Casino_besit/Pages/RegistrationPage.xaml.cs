using Casino_besit.DataBase;
using Casino_besit.Pages;
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
using Wpf.Ui.Controls;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private Users user;
        public RegistrationPage()
        {
            InitializeComponent();
            user = new Users();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = Login.Text;
                string password = PasswordBox.Password;
                string password1 = PasswordBox1.Password;
                if (string.IsNullOrEmpty(SecondNameTextBox.Text) || string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(PasswordBox.Password) || string.IsNullOrEmpty(PhoneNumberTextBox.Text))
                {
                    System.Windows.MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (ConnectionClass.DB.Users.Any(u => u.Login == login))
                {
                    System.Windows.MessageBox.Show("Пользователь с таким логином уже существует");
                    return;
                }
                else if (password != password1)
                {
                    System.Windows.MessageBox.Show("Пароли не совпадают");
                    return;
                }
                else
                {
                    user.RegistrationDate = DateTime.Now;
                    user.SecondName = SecondNameTextBox.Text;
                    user.FirstName = NameTextBox.Text;
                    user.Login = Login.Text;
                    user.Password = PasswordBox.Password;
                    user.PhoneNumber = PhoneNumberTextBox.Text;
                    user.RoleID = 2;
                    user.Balance = 0;
                    ConnectionClass.DB.Users.Add(user);
                    ConnectionClass.DB.SaveChanges();
                    System.Windows.MessageBox.Show("Регистрация прошла успешно");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"{ex.Message}", "Ошибка", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoginTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
        private void TxtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) || PhoneNumberTextBox.Text.Length >= 18)
            {
                e.Handled = true;
                return;
            }

            if (PhoneNumberTextBox.Text.Length == 0)
            {
                PhoneNumberTextBox.Text = "+7 (";
                PhoneNumberTextBox.SelectionStart = PhoneNumberTextBox.Text.Length;
            }
            else if (PhoneNumberTextBox.Text.Length == 7)
            {
                PhoneNumberTextBox.Text += ") ";
                PhoneNumberTextBox.SelectionStart = PhoneNumberTextBox.Text.Length;
            }
            else if (PhoneNumberTextBox.Text.Length == 12 || PhoneNumberTextBox.Text.Length == 15)
            {
                PhoneNumberTextBox.Text += "-";
                PhoneNumberTextBox.SelectionStart = PhoneNumberTextBox.Text.Length;
            }
        }
    }
}
