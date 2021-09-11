namespace Turtle.GameManagement
{
    using System;

    public static class SessionStatus
    {
        private static int escapes = 0;
        private static int escapesSpree = 0;
        private static int mineHits = 0;
        private static int boardDrops = 0;
        private static int turtleLost = 0;

        public static void AddEscape()
        {
            escapes += 1;
            escapesSpree += 1;
        }

        public static void AddMineHit()
        {
            mineHits += 1;
            escapesSpree = 0;
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
            Console.WriteLine($"Mine hits: {mineHits}");
            Console.WriteLine($"Board drops: {boardDrops}");
        }
    }
}