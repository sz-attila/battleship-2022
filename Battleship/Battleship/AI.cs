using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// A class for AI methods.
    /// </summary>
    public static class AI
    {
        /// <summary>
        /// Determines if a cell in a game table has already been shot.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <param name="table">The game table.</param>
        /// <returns>true if the cell has been shot, false otherwise.</returns>
        public static bool IsShootedCell(int x, int y, char[,] table)
        {
            return table[x, y] is 'H' or 'M';
        }

        /// <summary>
        /// Generates a random shoot on a game table.
        /// </summary>
        /// <param name="rnd">A Random object used to generate random coordinates.</param>
        /// <param name="table">The game table.</param>
        /// <returns>The index of the cell that was shot.</returns>
        public static int GenerateShoot(Random rnd, char[,] table)
        {
            int rndX;
            int rndY;

            do
            {
                rndX = rnd.Next(0, 10);
                rndY = rnd.Next(0, 10);
            }
            while (IsShootedCell(rndX, rndY, table));

            return (rndY * 10) + rndX;
        }

        /// <summary>
        /// Determines if a cell in a game table contains a player's unit.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <param name="table">The game table.</param>
        /// <returns>true if the cell contains a player's unit, false otherwise.</returns>
        public static bool IsPlayerUnit(int x, int y, char[,] table)
        {
            return char.IsDigit(table[x, y]);
        }

        /// <summary>
        /// Determines if a cell is outside the boundaries of a game table.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <returns>true if the cell is outside the boundaries of the game table, false otherwise.</returns>
        public static bool DetectBorder(int x, int y)
        {
            return x is < 0 or > 9 || y is < 0 or > 9;
        }

        /// <summary>
        /// Creates a new ship for a game grid.
        /// </summary>
        /// <param name="playfield">The game grid.</param>
        /// <returns>A new ship as a Rectangle object.</returns>
        public static Rectangle CreateShip(Grid playfield)
        {
           Rectangle ship = new()
           {
               Fill = Brushes.BlanchedAlmond
           };
            
           double x = playfield.Height / 10;
           double y = playfield.Width / 10;
            
           ship.Height = x;
           ship.Width = y;
           ship.Visibility = Visibility.Hidden;
            
           return ship;
        }

        /// <summary>
        /// Generates a game table for the AI player and displays it on a game grid.
        /// </summary>
        /// <param name="rnd">A Random object used to generate random coordinates and orientations for the ships.</param>
        /// <param name="aiTable">The game table for the AI player.</param>
        /// <param name="playfield">The game grid.</param>
        public static void GenerateAItable(Random rnd, char[,] aiTable, Grid playfield)
        {
           int orient;
           int randomX;
           int randomY;
           bool empty;
            
           for (int i = 5; i > 0; i--)
           {
               empty = false;
               orient = rnd.Next(0, 2);
                
               if (orient == 0)
               {
                   randomX = rnd.Next(0, 10);
                   randomY = rnd.Next(0, 10 - i + 1);
                    
                   while (empty == false)
                   {
                        if ((randomY != 0 && char.IsDigit(aiTable[randomY - 1, randomX])) || (randomY + i - 1 != 9 && char.IsDigit(aiTable[randomY + i, randomX])))
                        {
                           randomX = rnd.Next(0, 10);
                           randomY = rnd.Next(0, 10 - i + 1);
                        }
                        else
                        {
                           for (int j = 0; j < i; j++)
                           {
                                if (char.IsDigit(aiTable[randomY + j, randomX]) || (randomX != 0 && char.IsDigit(aiTable[randomY + j, randomX - 1])) || (randomX != 9 && char.IsDigit(aiTable[randomY + j, randomX + 1])))
                                {
                                   randomX = rnd.Next(0, 10);
                                   randomY = rnd.Next(0, 10 - i + 1);
                                    
                                   break;
                                }
                                else if (j == (i - 1))
                                {
                                   empty = true;
                                }
                           }
                        }
                   }
                   
                   for (int row = 0; row < i; row++)
                   {
                       Rectangle ship = CreateShip(playfield);

                       Grid.SetColumn(ship, row + randomY);
                       Grid.SetRow(ship, randomX);
                        
                       aiTable[randomY + row, randomX] = Convert.ToChar(i.ToString());
                       playfield.Children.Add(ship);
                   }
               }
               else
               {
                    randomX = rnd.Next(0, 10 - i + 1);
                    randomY = rnd.Next(0, 10);

                    while (empty == false)
                    {
                        if ((randomX != 0 && char.IsDigit(aiTable[randomY, randomX - 1])) || ((randomX + i - 1) != 9 && char.IsDigit(aiTable[randomY, randomX + i])))
                        {
                            randomX = rnd.Next(0, 10 - i + 1);
                            randomY = rnd.Next(0, 10);
                        }
                        else
                        {
                            for (int j = 0; j < i; j++)
                            {
                                if (char.IsDigit(aiTable[randomY, randomX + j]) || (randomY != 0 && char.IsDigit(aiTable[randomY - 1, randomX + j])) || (randomY != 9 && char.IsDigit(aiTable[randomY + 1, randomX + j])))
                                {
                                    randomX = rnd.Next(0, 10 - i + 1);
                                    randomY = rnd.Next(0, 10);

                                    break;
                                }
                                else if (j == (i - 1))
                                {
                                    empty = true;
                                }
                            }
                        }
                    }

                    for (int col = 0; col < i; col++)
                    {
                        Rectangle ship = CreateShip(playfield);

                        Grid.SetColumn(ship, randomY);
                        Grid.SetRow(ship, col + randomX);

                        aiTable[randomY, col + randomX] = Convert.ToChar(i.ToString());
                        playfield.Children.Add(ship);
                    }
               }
           }
        }
    }
}