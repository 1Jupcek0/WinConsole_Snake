using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp3 {
    class Fruit {
        const ConsoleColor FRUIT_COLOUR = ConsoleColor.Red;
        const ConsoleColor BACK_COLOUR = ConsoleColor.Black;
        const int fruitPosBuffer = 2;

        public int x = 10;
        public int y = 10;

        public static void Show(Fruit f) {
            Console.SetCursorPosition(f.x, f.y);
            Console.BackgroundColor = FRUIT_COLOUR;
            Console.WriteLine(" ");
            Console.BackgroundColor = BACK_COLOUR;
        }

        public static void NewPosition(Fruit f, Snake s) {
            int newX;
            int newY;
            Random pos = new Random();

            do {
                newX = pos.Next(fruitPosBuffer, Program.SCREEN_W - fruitPosBuffer);
                newY = pos.Next(fruitPosBuffer, Program.SCREEN_H - fruitPosBuffer);
            } while (ValidPos(f, s, newX, newY) == false);

            f.x = newX;
            f.y = newY;
        }

        static bool ValidPos(Fruit f, Snake s, int newX, int newY) {
            if (newX == f.x || newY == f.y) {
                return false;
            }
            if (s.xPositions.Contains(newX) || s.yPositions.Contains(newY)) {
                return false;
            } else {
                return true;
            }
        }

    }
}
