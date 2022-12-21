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
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for ScoreboardWindow.xaml
    /// </summary>
    public partial class ScoreboardWindow : Window
    {
        public List<Game> AllGames { get; set; }

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
    }
}