using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for ScoreboardWindow.xaml
    /// </summary>
    public partial class ScoreboardWindow : Window
    {
        private List<Game> AllGames { get; set; }

        /// <summary>
        /// Represents a window that displays a scoreboard of games.
        /// </summary>
        public ScoreboardWindow()
        {
            InitializeComponent();
            using (GameDbContext _context = new())
            {
                AllGames = _context.Games.ToList();
            }

            GamesList.ItemsSource = AllGames;

        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            Close();
            main.Show();
        }
        
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DbHelper.ClearDb();

            using GameDbContext database = new();
            AllGames = database.Games.ToList();
            GamesList.ItemsSource = AllGames;
        }
    }
}