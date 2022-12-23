using System.Windows;

namespace Battleship
{
    /// <summary>
    /// A class which contains common methods for a game.
    /// </summary>
    public static class Rules
    {
        public static void EndGame(string player1, string player2, int rounds, int player1Hits, int player2Hits, string winner)
        {
            _ = MessageBox.Show("Congratulations! {0} won!", winner);
            DbHelper.InsertToDb(player1, player2, rounds, player1Hits, player2Hits, winner);
        }
    }
}