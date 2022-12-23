using System;

namespace Battleship
{
    public static class AI
    {
        public static bool IsShootedCell(int x, int y, char[,] table)
        {
            return table[x, y] is 'S' or 'H';
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
    }
}