using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    /// <summary>
    /// A class which consists useful methods for database.
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// Inserts a new game record into the database.
        /// </summary>
        /// <param name="player1">The name of the first player.</param>
        /// <param name="player2">The name of the second player.</param>
        /// <param name="rounds">The number of rounds played in the game.</param>
        /// <param name="player1Hits">The number of hits scored by the first player.</param>
        /// <param name="player2Hits">The number of hits scored by the second player.</param>
        /// <param name="winner">The name of the winner of the game.</param>
        public static void InsertToDb(string player1, string player2, int rounds, int player1Hits, int player2Hits, string winner)
        {
            using GameDbContext database = new();
            database.Games.Add(new Game { Player1 = player1, Player2 = player2, Rounds = rounds, Player1Hits = player1Hits, Player2Hits = player2Hits, Winner = winner });

            database.SaveChanges();
        }

        /// <summary>
        /// Clears all game records from the database.
        /// </summary>
        public static void ClearDb()
        {
            using GameDbContext database = new();
            database.Games.RemoveRange(database.Games);

            database.SaveChanges();
        }
    }
}
