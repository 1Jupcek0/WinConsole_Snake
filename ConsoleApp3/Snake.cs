using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp3 {
    class Snake {
        const double SPEED_INCREMENT = 0.3;
        const int START_LENGTH = 10;
        const char SNAKE_CHAR = '@';
        const ConsoleColor SNAKE_COLOUR = ConsoleColor.Green;

        public int x;
        public int y;
        public int xDir;
        public int yDir;
        public int score;
        public int highScore = 0;
        public double speed;
        public bool doGrow;
        public bool dead;
        public List<int> xPositions = new List<int>();
        public List<int> yPositions = new List<int>();

        public static void InitSnake(Snake s) {
            s.x = 9;
            s.y = 3;
            s.xDir = 1;
            s.yDir = 0;
            s.score = 0;
            s.speed = 1.2;
            s.doGrow = false;
            s.dead = false;
            s.xPositions.Clear();
            s.yPositions.Clear();

            for (int i = START_LENGTH - 1; i >= 0; i--) {
                s.xPositions.Add(s.x - i);
                s.yPositions.Add(s.y);
            }
        }

        public static void SetDir(int x, int y, Snake s) {
            if (s.xDir != -x && s.yDir != -y) {
                s.xDir = x;
                s.yDir = y;
            }
        }

        public static void Update(Snake s) {
            s.x = s.x + s.xDir;
            s.y = s.y + s.yDir;

            if (IsGameOver(s)) {
                s.dead = true;
            }

            s.xPositions.Add(s.x);
            s.yPositions.Add(s.y);
            if (!s.doGrow) {
                s.xPositions.RemoveAt(0);
                s.yPositions.RemoveAt(0);
            }
        }

        public static void Eat(Snake s, Fruit f) {
            if (s.x == f.x && s.y == f.y) {
                s.speed = s.speed + SPEED_INCREMENT;
                s.score++;
                Fruit.NewPosition(f, s);
                s.doGrow = true;
            }
        }

        public static void Show(Snake s) {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < s.xPositions.Count(); i++) {
                Console.SetCursorPosition(s.xPositions[i], s.yPositions[i]);
                Console.Write(SNAKE_CHAR);
            }
        }

        public static void ShowScore(Snake s) {
            Console.ForegroundColor = SNAKE_COLOUR;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Score: " + s.score);
        }

        static bool IsGameOver(Snake s) {
            if (s.x < 0 || s.x > Program.SCREEN_W - 1 || s.y < 0 || s.y > Program.SCREEN_H - 1) {
                return true;
            }
            for (int i = 0; i < s.xPositions.Count(); i++) {
                if (s.xPositions[i] == s.x && s.yPositions[i] == s.y) {
                    return true;
                }
            }
            return false;
        }
    }
}
