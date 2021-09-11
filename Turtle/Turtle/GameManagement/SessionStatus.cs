namespace Turtle.GameManagement
{
    using System;

    public static class SessionStatus
    {
        private static int escapes = 0;
        private static int escapesSpree = 0;
        private static int minesDeaths = 0;
        private static int minesEaten = 0;
        private static int applesEaten = 0;
        private static int boardDrops = 0;
        private static int turtleLost = 0;

        public static void AddEscape()
        {
            escapes += 1;
            escapesSpree += 1;
        }

        public static void AddMineDeath()
        {
            minesDeaths += 1;
            escapesSpree = 0;
        }

        public static void AddMinesEaten()
        {
            minesEaten += 1;
        }

        public static void AddApplesEaten()
        {
            applesEaten += 1;
        }

        public static void AddBoardDrop()
        {
            boardDrops += 1;
            escapesSpree = 0;
        }

        public static void AddTurtleLost()
        {
            turtleLost += 1;
            escapesSpree = 0;
        }

        public static void Draw()
        {
            Console.WriteLine("Game session status");
            Console.WriteLine($"Escapes: {escapes}");
            Console.WriteLine($"Escape Spree: {escapesSpree}");
            Console.WriteLine($"Mine deaths: {minesDeaths}");
            Console.WriteLine($"Mine eaten: {minesEaten}");
            Console.WriteLine($"Apples eaten: {applesEaten}");
            Console.WriteLine($"Board drops: {boardDrops}");
            Console.WriteLine($"Turtles lost in the board: {boardDrops}");
        }
    }
}