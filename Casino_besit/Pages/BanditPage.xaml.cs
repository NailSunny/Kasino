using Casino_besit.DataBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Casino_besit.Pages
{
    public partial class BanditPage : Page
    {
        private Random random = new Random();
        private int betAmount;
        private int winnings;
        private string[] symbols = new string[] { "🍒", "🍋", "🍇", "🎉", "💎", "🔥" };
        private string[] reel1 = new string[3];
        private string[] reel2 = new string[3];
        private string[] reel3 = new string[3];
        private readonly string _password;  // Пароль пользователя
        private int sessionID;



        public BanditPage(string password)
        {
            _password = password;
            // Предполагаем, что _currentUser и _currentGame инициализируются глобально, например, из статических полей
     

            InitializeComponent();
        }

        // Логика для кнопки "Назад"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        // Логика для кнопки "Вращать"
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
                            // Вращаем барабаны
                            SpinReels();

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
        private void SpinReels()
        {
            // Вращаем барабаны
            for (int i = 0; i < 3; i++)
            {
                reel1[i] = symbols[random.Next(0, symbols.Length)];
                reel2[i] = symbols[random.Next(0, symbols.Length)];
                reel3[i] = symbols[random.Next(0, symbols.Length)];
            }

            // Обновляем барабаны на форме
            Reel1.Text = reel1[1];
            Reel2.Text = reel2[1];
            Reel3.Text = reel3[1];
        }
        private void CheckWin()
        {
            // Проверяем выигрыш
            if (reel1[1] == reel2[1] && reel2[1] == reel3[1])
            {
                // Выигрыш!
                winnings = betAmount * 10;
                MessageTbx.Text = "Вы выиграли!";
                resultMessage = "Выигрыш";
            }
            else if (reel1[1] == reel2[1] || reel2[1] == reel3[1] || reel1[1] == reel3[1])
            {
                // Выигрыш!
                winnings = betAmount * 5;
                MessageTbx.Text = "Вы выиграли!";
                resultMessage = "Выигрыш";
            }
            else
            {
                // Проигрыш
                winnings = 0;
                MessageTbx.Text = "Вы проиграли!";
                resultMessage = "Проигрыш";
            }
        }
        // Метод для расчета выигрыша
        private void SaveGameSession()
        {
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (user != null)
            {
                var session = new Game_Sessions
                {
                    User_ID = user.User_ID,
                    Game_ID = 2, // ID для игры "Однорукий бандит"
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
        string resultMessage = "";

        private void SaveGameHistory()
        {
            var session = ConnectionClass.DB.Game_Sessions.Find(sessionID);
            var user = ConnectionClass.DB.Users.FirstOrDefault(u => u.Password == _password);
            if (session != null)
            {
                
                user.Balance += Convert.ToInt32(winnings);
                ConnectionClass.DB.SaveChanges();
                session.Win_Amount = winnings > 0 ? Convert.ToInt32(winnings) : 0;
                ConnectionClass.DB.SaveChanges();

                var gameHistory = new Game_History
                {
                    User_ID = session.User_ID,
                    Type = resultMessage,
                    Amount = Convert.ToInt32(winnings),
                    HistoryDate = DateTime.Now
                };
                ConnectionClass.DB.Game_History.Add(gameHistory);
                ConnectionClass.DB.SaveChanges();
            }
        }
        
    }
}
