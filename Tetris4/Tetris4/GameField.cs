using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4
{
    internal class GameField
    {
        public static int Width => 10;
        public static int Height => 20;

        private bool[,] field = new bool[Height, Width];

        public bool this[int y, int x]
        {
            get => field[y, x];
            set => field[y, x] = value;
        }

        public bool CheckCollision(Figure figure, int offsetX, int offsetY)
        {
            for (int y = 0; y < figure.Height; y++)
                for (int x = 0; x < figure.Width; x++)
                    if (figure.Shape[y, x] &&
                        (figure.Position.y + y + offsetY >= Height ||
                         figure.Position.x + x + offsetX >= Width ||
                         figure.Position.x + x + offsetX < 0 ||
                         field[figure.Position.y + y + offsetY, figure.Position.x + x + offsetX]))
                        return true;
            return false;
        }

        public void MergeFigure(Figure figure)
        {
            for (int y = 0; y < figure.Height; y++)
                for (int x = 0; x < figure.Width; x++)
                    if (figure.Shape[y, x])
                        field[figure.Position.y + y, figure.Position.x + x] = true;
        }

        public void ClearFullRows()
        {
            for (int y = 0; y < Height; y++)
            {
                bool fullRow = true;
                for (int x = 0; x < Width; x++)
                    if (!field[y, x])
                    {
                        fullRow = false;
                        break;
                    }
                if (fullRow)
                {
                    for (int moveY = y; moveY > 0; moveY--)
                        for (int x = 0; x < Width; x++)
                            field[moveY, x] = field[moveY - 1, x];
                }
            }
        }

        public void Draw(Figure figure)
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (figure.Position.y <= y && y < figure.Position.y + figure.Height &&
                        figure.Position.x <= x && x < figure.Position.x + figure.Width &&
                        figure.Shape[y - figure.Position.y, x - figure.Position.x])
                        Console.Write("#");
                    else
                        Console.Write(field[y, x] ? "#" : ".");
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }
}
