using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp3 {
    class Program {
        const int SCREEN_MULT = 5;
        public const int SCREEN_W = 12 * SCREEN_MULT;
        public const int SCREEN_H = 5 * SCREEN_MULT;

        static void Main(string[] args) {
            var s = new Snake();
            var f = new Fruit();
            bool escape = false;

            InitGame(s, f);
            escape = Welcome(escape);

            while (!escape) {
                ResetGame(s, f);
                escape = PlayGame(s, f, escape);
                if (!escape) {
                    escape = DoGameOver(s, escape);
                }
            }
        }

        static bool Welcome(bool escape) {
            ConsoleKeyInfo keyInfo;

            string[] Instructions = { "Welcome to the snake game", "Your goal is to collect the red fruit", "Use the arrow keys to move around", "Press esc to exit", "Press any key to begin" };

            for (int i = 0; i < Instructions.Count(); i++) {
                Console.SetCursorPosition((SCREEN_W / 2) - ((Instructions[i].Length / 2)), (SCREEN_H / 2) + i - (Instructions.Count() / 2));
                Console.WriteLine(Instructions[i]);
            }

            if ((keyInfo = Console.ReadKey(true)).Key == ConsoleKey.Escape) {
                return true;
            }

            return false;
        }

        static void InitGame(Snake s, Fruit f) {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WindowHeight = SCREEN_H;
            Console.WindowWidth = SCREEN_W;
        }

        static bool PlayGame(Snake s, Fruit f, bool escape) {
            ConsoleKeyInfo keyInfo;
            Snake.ShowScore(s);

            while (true) {
                s.doGrow = false;

                Thread.Sleep(Convert.ToInt16(100 / s.speed));
                Console.Clear();
                if (Console.KeyAvailable == true) {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key) {
                        case ConsoleKey.UpArrow:
                            Snake.SetDir(0, -1, s);
                            break;
                        case ConsoleKey.DownArrow:
                            Snake.SetDir(0, 1, s);
                            break;
                        case ConsoleKey.RightArrow:
                            Snake.SetDir(1, 0, s);
                            break;
                        case ConsoleKey.LeftArrow:
                            Snake.SetDir(-1, 0, s);
                            break;
                        case ConsoleKey.Escape:
                            escape = true;
                            return escape;
                    }
                }
                Snake.Eat(s, f);
                Snake.Update(s);
                if (s.dead) {
                    break;
                }
                Snake.ShowScore(s);
                Fruit.Show(f);
                Snake.Show(s);
            }
            return false;
        }

        static bool DoGameOver(Snake s, bool escape) {
            string[] Instructions = { "Game  Over", "Score: " + s.score };
            ConsoleKeyInfo keyInfo;

            for (int i = 0; i < Instructions.Count(); i++) {
                Console.SetCursorPosition((SCREEN_W / 2) - ((Instructions[i].Length / 2)), (SCREEN_H / 2) + i - (Instructions.Count() / 2));
                Console.WriteLine(Instructions[i]);
            }

            if (s.score > s.highScore) {
                s.highScore = s.score;
                Console.SetCursorPosition((SCREEN_W / 2) - 7, (SCREEN_H / 2) + 2);
                Console.WriteLine("NEW HIGH SCORE!!!");
            }

            Console.SetCursorPosition((SCREEN_W / 2) - 6, (SCREEN_H / 2) + 3);
            Console.WriteLine("Highscore: " + s.highScore);
            Thread.Sleep(3000);
            if (!escape) {
                Console.Clear();
                Console.SetCursorPosition((SCREEN_W / 2) - 8, (SCREEN_H / 2) - 1);
                Console.WriteLine("Press esc to exit");
                Console.SetCursorPosition((SCREEN_W / 2) - 12, (SCREEN_H / 2) + 1);
                Console.WriteLine("Or press any key play again");
                if ((keyInfo = Console.ReadKey(true)).Key == ConsoleKey.Escape) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }

        static void ResetGame(Snake s, Fruit f) {
            Fruit.NewPosition(f, s);
            Snake.InitSnake(s);
        }
    }

    
}