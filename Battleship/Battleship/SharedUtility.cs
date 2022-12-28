using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Battleship
{
    /// <summary>
    /// A class which contains common methods for a game.
    /// </summary>
    public static class SharedUtility
    {
        private static Random rnd = new();
        public const int ROWS = 10;
        public const int COLUMNS = 10;

        /// <summary>
        /// Ends a game and persists the game's results to a database.
        /// </summary>
        /// <param name="player1">The name of the first player.</param>
        /// <param name="player2">The name of the second player.</param>
        /// <param name="rounds">The number of rounds played in the game.</param>
        /// <param name="player1Hits">The number of hits made by the first player.</param>
        /// <param name="player2Hits">The number of hits made by the second player.</param>
        /// <param name="winner">The name of the winning player.</param>
        public static void EndGame(string player1, string player2, int rounds, int player1Hits, int player2Hits, string winner)
        {
            _ = MessageBox.Show("Game Over!");
            DbHelper.InsertToDb(player1, player2, rounds, player1Hits, player2Hits, winner);
        }

        /// <summary>
        /// Determines which player should start a game.
        /// </summary>
        /// <returns>`true` if the first player should start, `false` if the second player should start.</returns>
        public static bool WhichPlayerStart()
        {
            return rnd.Next(0, 2) == 0;
        }

        /// <summary>
        /// Loads player ships onto a grid.
        /// </summary>
        /// <param name="playfield">The grid where the ships are currently located.</param>
        /// <param name="table">The grid where the ships should be moved to.</param>
        public static void PlayerShipsLoad(Grid playfield, Grid table)
        {
            for (int unit = playfield.Children.Count - 1; unit >= 0; unit--)
            {
                UIElement child = playfield.Children[unit];
                playfield.Children.RemoveAt(unit);
                table.Children.Add(child);
            }
        }

        /// <summary>
        /// Initializes the hit points for the ships in a game.
        /// </summary>
        /// <param name="carrierHpGrid">The grid that displays the hit points for the carrier.</param>
        /// <param name="battleshipHpGrid">The grid that displays the hit points for the battleship.</param>
        /// <param name="cruiserHpGrid">The grid that displays the hit points for the cruiser.</param>
        /// <param name="submarineHpGrid">The grid that displays the hit points for the submarine.</param>
        /// <param name="destroyerHpGrid">The grid that displays the hit points for the destroyer.</param>
        public static void ShipStatHpInit(Grid carrierHpGrid, Grid battleshipHpGrid, Grid cruiserHpGrid, Grid submarineHpGrid, Grid destroyerHpGrid)
        {
            for (int ship = 5; ship > 0; ship--)
            {
                for (int unit = 0; unit < ship; unit++)
                {
                    Rectangle hpUnit = ShipHpSettings(ship, carrierHpGrid);

                    Grid.SetColumn(hpUnit, unit);

                    switch (ship)
                    {
                        case 5:
                            carrierHpGrid.Children.Add(hpUnit);
                            break;
                        case 4:
                            battleshipHpGrid.Children.Add(hpUnit);
                            break;
                        case 3:
                            cruiserHpGrid.Children.Add(hpUnit);
                            break;
                        case 2:
                            submarineHpGrid.Children.Add(hpUnit);
                            break;
                        case 1:
                            destroyerHpGrid.Children.Add(hpUnit);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Configures the appearance of a rectangle that represents a unit of hit points for a ship.
        /// </summary>
        /// <param name="shipLength">The length of the ship.</param>
        /// <param name="carrierHpGrid">The grid that displays the hit points for the carrier.</param>
        /// <returns>The configured rectangle.</returns>
        public static Rectangle ShipHpSettings(int shipLength, Grid carrierHpGrid)
        {
            Rectangle hpUnit = new()
            {
                Fill = Brushes.Green,
                RadiusX = 2,
                RadiusY = 2
            };
            double Y = carrierHpGrid.Width;
            double X = carrierHpGrid.Height / shipLength;
            hpUnit.Width = Y;
            hpUnit.Height = X;
            return hpUnit;
        }

        /// <summary>
        /// Updates the rounds label in a game.
        /// </summary>
        /// <param name="roundsLabel">The label that displays the number of rounds played in the game.</param>
        /// <param name="playerChangeCounter">A reference to a counter that keeps track of the number of times a player has taken a turn.</param>
        /// <param name="rounds">A reference to a variable that stores the current number of rounds played in the game.</param>
        public static void RoundsLabelChange(Label roundsLabel, ref int playerChangeCounter, ref int rounds)
        {
            playerChangeCounter++;

            if (playerChangeCounter % 2 == 0)
            {
                rounds++;
                roundsLabel.Content = rounds;
            }
        }

        /// <summary>
        /// Decrements the hit points of a ship in a game.
        /// </summary>
        /// <param name="shipUnitName">The length of the ship that has been hit.</param>
        /// <param name="carrierHpGrid">The grid that displays the hit points for the carrier.</param>
        /// <param name="battleshipHpGrid">The grid that displays the hit points for the battleship.</param>
        /// <param name="cruiserHpGrid">The grid that displays the hit points for the cruiser.</param>
        /// <param name="submarineHpGrid">The grid that displays the hit points for the submarine.</param>
        /// <param name="destroyerHpGrid">The grid that displays the hit points for the destroyer.</param>
        public static void ShipHpDecrement(string shipUnitName, Grid carrierHpGrid, Grid battleshipHpGrid, Grid cruiserHpGrid, Grid submarineHpGrid, Grid destroyerHpGrid)
        {
            switch (shipUnitName)
            {
                case "5":
                    carrierHpGrid.Children.RemoveAt(carrierHpGrid.Children.Count - 1);
                    break;
                case "4":
                    battleshipHpGrid.Children.RemoveAt(battleshipHpGrid.Children.Count - 1);
                    break;
                case "3":
                    cruiserHpGrid.Children.RemoveAt(cruiserHpGrid.Children.Count - 1);
                    break;
                case "2":
                    submarineHpGrid.Children.RemoveAt(submarineHpGrid.Children.Count - 1);
                    break;
                case "1":
                    destroyerHpGrid.Children.RemoveAt(destroyerHpGrid.Children.Count - 1);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Determines which cell the cursor is currently on in a grid.
        /// </summary>
        /// <param name="rightTable">The grid that the cursor is interacting with.</param>
        /// <returns>The index of the cell as an integer.</returns>
        public static int CalculateCell(Grid rightTable) //which cell the cursor is on
        {
            Point point = Mouse.GetPosition(rightTable);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            foreach (RowDefinition rowDefinition in rightTable.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            foreach (ColumnDefinition columnDefinition in rightTable.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            return (row * 10) + col;
        }
    }
}