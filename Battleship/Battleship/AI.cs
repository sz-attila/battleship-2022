using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Battleship
{
    public static class AI
    {
        public static bool IsShootedCell(int x, int y, char[,] table)
        {
            return table[x, y] is 'H' or 'M';
        }

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

        public static bool IsPlayerUnit(int x, int y, char[,] table)
        {
            return char.IsDigit(table[x, y]);
        }

        public static bool DetectBorder(int x, int y)
        {
            return x is < 0 or > 9 || y is < 0 or > 9;
        }
        public static Rectangle CreateShip(int shipSize, Grid playfield)
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
                       Rectangle ship = CreateShip(i, playfield);
                        
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
                        Rectangle ship = CreateShip(i, playfield);

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