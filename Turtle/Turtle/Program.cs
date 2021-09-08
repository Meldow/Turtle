namespace Turtle
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        public static async Task Main(string[] args)
        {
            // Board setup
            var inputFileGameSettings = new StreamReader(args[0]);

            var boardInput = await inputFileGameSettings.ReadLineAsync();
            var boardSize = boardInput.Split(',');
            var board = new Board2(int.Parse(boardSize[0]), int.Parse(boardSize[1]));

            var turtleLocationInput = await inputFileGameSettings.ReadLineAsync();
            var turtleLocation = turtleLocationInput.Split(',');

            board.Turtle = new Turtle(
                int.Parse(turtleLocation[0]),
                int.Parse(turtleLocation[1]),
                Enum.Parse<DirectionEnum>(turtleLocation[2]));

            string mine;
            while ((mine = inputFileGameSettings.ReadLine()) != null)
            {
                var mineLocation = mine.Split(',');
                board.AddMine(int.Parse(mineLocation[0]), int.Parse(mineLocation[1]));
            }

            inputFileGameSettings.Close();

            // Game Loop
            var inputMoves = new StreamReader(args[1]);

            string move;
            while ((move = await inputMoves.ReadLineAsync()) != null)
            {
                // Validate if move is possible
                    
                // Validate collision

                // Move turtle to new location
            }

            inputMoves.Close();

            Console.WriteLine("Hello World!");
        }
    }
}