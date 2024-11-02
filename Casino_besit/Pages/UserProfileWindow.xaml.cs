using Casino_besit.DataBase;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using QRCoder;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private string _password;
        public UserProfileWindow(string password)
        {
            InitializeComponent();
            _password = password;
            var Obj = ConnectionClass.DB.Users.Where(Users => Users.Password == _password).FirstOrDefault();
            
            QRCodeImage.Source = GeneratorQrCodeBitmapImage($" Логин: {Obj.Login}," + $" Фамилия: {Obj.SecondName}," + $" Имя: {Obj.FirstName}," + $" Номер телефона: {Obj.PhoneNumber}," + $" Баланс: {Obj.Balance}");
            this.DataContext = MainWindow.user;
        }
        private BitmapImage GeneratorQrCodeBitmapImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q); using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrBitmap.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            BitmapImage bitmapImage = new BitmapImage(); bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; bitmapImage.StreamSource = ms;
                            bitmapImage.EndInit();
                            return bitmapImage;
                        }
                    }
                }
            }
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
