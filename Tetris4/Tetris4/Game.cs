using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4
{
    internal class Game
    {
        private GameField gameField = new GameField();
        private Figure currentFigure = new Figure();
        private bool gameRunning = true;
        private int fallDelay = 500; // Milliseconds

        public void Run()
        {
            Thread fallThread = new Thread(FallLoop);
            fallThread.Start();

            while (gameRunning)
            {
                if (!Console.KeyAvailable) continue;
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (!gameField.CheckCollision(currentFigure, -1, 0))
                            currentFigure.Position = (currentFigure.Position.x - 1, currentFigure.Position.y);
                        break;
                    case ConsoleKey.RightArrow:
                        if (!gameField.CheckCollision(currentFigure, 1, 0))
                            currentFigure.Position = (currentFigure.Position.x + 1, currentFigure.Position.y);
                        break;
                    case ConsoleKey.DownArrow:
                        if (!gameField.CheckCollision(currentFigure, 0, 1))
                            currentFigure.Position = (currentFigure.Position.x, currentFigure.Position.y + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        currentFigure.Rotate();
                        if (gameField.CheckCollision(currentFigure, 0, 0))
                            currentFigure.Rotate(); // Undo if collision
                        break;
                }
                gameField.Draw(currentFigure);
            }
        }

        private void FallLoop()
        {
            while (gameRunning)
            {
                Thread.Sleep(fallDelay);
                if (!gameField.CheckCollision(currentFigure, 0, 1))
                {
                    currentFigure.Position = (currentFigure.Position.x, currentFigure.Position.y + 1);
                }
                else
                {
                    gameField.MergeFigure(currentFigure);
                    gameField.ClearFullRows();
                    currentFigure = new Figure();

                    if (gameField.CheckCollision(currentFigure, 0, 0))
                    {
                        gameRunning = false;
                        Console.Clear();
                        Console.WriteLine("Game Over");
                    }
                }
                gameField.Draw(currentFigure);
            }
        }
    }
}
