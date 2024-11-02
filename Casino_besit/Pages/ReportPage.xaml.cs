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
using Casino_besit.Pages;
using LiveCharts.Wpf;
using LiveCharts;
using Casino_besit.DataBase;

namespace Casino_besit.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    { 
        public SeriesCollection PieSeriesCollection { get; set; }
        private string _password;
        public ReportPage(string password)
        {
            InitializeComponent();
            _password = password;
            var user = ConnectionClass.DB.Users.Where(p => p.Password == _password).FirstOrDefault();
            var totalBets = ConnectionClass.DB.Game_Sessions.Where(g => g.User_ID == user.User_ID && g.Bet_Amount.HasValue).Sum(g => g.Bet_Amount.Value);
            var totalWins = ConnectionClass.DB.Game_Sessions.Where(g => g.User_ID == user.User_ID && g.Win_Amount.HasValue).Sum(g => g.Win_Amount.Value);
            
            stavkaTxt.Text = Convert.ToString(totalBets);
            winingTxt.Text = Convert.ToString(totalWins);
            lossTxt.Text = ConnectionClass.DB.Game_Sessions.Where(s => s.Win_Amount == 0 && s.Bet_Amount.HasValue).Sum(s =>  s.Bet_Amount.Value).ToString();
                       // Инициализация данных для круговой диаграммы
                       PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Выигрыши",
                    Values = new ChartValues<double> { (int)totalWins }, // Значение общего выигрыша
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 204, 0))
                },
                new PieSeries
                {
                    Title = "Проигрыши",
                    Values = new ChartValues<double> { Convert.ToInt32( lossTxt.Text) }, // Значение общего проигрыша
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 69, 0))
                }
            };

            // Привязка данных к DataContext для привязки в XAML
            DataContext = this;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
