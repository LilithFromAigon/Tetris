using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4
{
    internal class Figure
    {
        private static readonly List<bool[,]> Shapes = new List<bool[,]>()
        {
            new bool[2, 2] { { true, true }, { true, true } }, // Square
            new bool[1, 4] { { true, true, true, true } }, // Line
            new bool[2, 3] { { false, true, false }, { true, true, true } }, // T
            new bool[2, 3] { { true, true, false }, { false, true, true } }, // Z
            new bool[2, 3] { { false, true, true }, { true, true, false } }  // S
        };

        public bool[,] Shape { get; private set; }
        public int Width => Shape.GetLength(1);
        public int Height => Shape.GetLength(0);
        public (int x, int y) Position { get; set; }

        public Figure()
        {
            Shape = Shapes[new Random().Next(Shapes.Count)];
            Position = (GameField.Width / 2 - Width / 2, 0);
        }

        public void Rotate()
        {
            bool[,] newShape = new bool[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    newShape[x, Height - y - 1] = Shape[y, x];
            Shape = newShape;
        }
    }
}
