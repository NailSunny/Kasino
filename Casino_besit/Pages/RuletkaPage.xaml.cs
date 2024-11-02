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
    /// Логика взаимодействия для RuletkaPage.xaml
    /// </summary>
    public partial class RuletkaPage : Page
    {
        private Random random = new Random();
        private int betAmount;
        private int winnings;
        private readonly string _password;  // Пароль пользователя
        private int sessionID;

        public RuletkaPage(string password)
        {
            _password = password;
            InitializeComponent();
        }

        // Логика для кнопки "Назад"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        // Логика для кнопки "Запустить рулетку"
        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(BetAmountTxt.Text))
                {
                    betAmount = int.Parse(BetAmountTxt.Text);
                    var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
                    if (user != null)
                    {
                        if (betAmount > user.Balance)
                        {
                            MessageTbx.Text = "У вас недостаточно средств для этой ставки!";
                        }
                        else if (betAmount > 0)
                        {
                            // Вращаем рулетку
                            SpinRoulette();

                            // Проверяем выигрыш
                            CheckWin();

                            // Сохраняем результаты игры в базу данных
                            SaveGameSession();
                            SaveGameHistory();

                            // Обновляем вывод выигрыша
                            WinText.Text = $"Выигрыш: {winnings}";
                        }
                        else
                            MessageTbx.Text = "Вы не можете поставить такую ставку!";
                    }
                }
                else
                    MessageTbx.Text = "Для игры нужна ставка!";
            }
            catch (FormatException)
            {
                MessageTbx.Text = "Некорректная ставка!";
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так!");
            }
        }

        private void SpinRoulette()
        {
            // Вращение рулетки, случайное число от 0 до 36
            int result = random.Next(0, 37);
            RouletteResult.Text = result.ToString();
        }

        private void CheckWin()
        {
            // Проверка на выигрыш (например, если выпадает четное число)
            int result = int.Parse(RouletteResult.Text);

            if (result % 2 == 0)
            {
                // Выигрыш!
                winnings = betAmount * 2;
                MessageTbx.Text = "Вы выиграли!";
            }
            else
            {
                // Проигрыш
                winnings = 0;
                MessageTbx.Text = "Вы проиграли!";
            }
        }

        private void SaveGameSession()
        {
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (user != null)
            {
                var session = new Game_Sessions
                {
                    User_ID = user.User_ID,
                    Game_ID = 3, // ID для игры "Рулетка"
                    Bet_Amount = betAmount,
                    Win_Amount = winnings,
                    Date = DateTime.Now
                };
                ConnectionClass.DB.Game_Sessions.Add(session);
                ConnectionClass.DB.SaveChanges();
                sessionID = session.Session_ID;

                // Обновляем баланс пользователя
                user.Balance += winnings - betAmount;
                ConnectionClass.DB.SaveChanges();
            }
        }

        private void SaveGameHistory()
        {
            var session = ConnectionClass.DB.Game_Sessions.Find(sessionID);
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (session != null)
            {
                var gameHistory = new Game_History
                {
                    User_ID = session.User_ID,
                    Type = winnings > 0 ? "Выигрыш" : "Проигрыш",
                    Amount = winnings,
                    HistoryDate = DateTime.Now
                };
                ConnectionClass.DB.Game_History.Add(gameHistory);
                ConnectionClass.DB.SaveChanges();
            }
        }
    }
}
