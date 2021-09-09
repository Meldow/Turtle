namespace Turtle
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

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

            var inputMoves = new StreamReader(args[1]);
            // Game Loop
            try
            {
                string move;
                while ((move = await inputMoves.ReadLineAsync()) != null)
                {
                    var destination = board.GetObject(board.Turtle.Forward());

                    // Move turtle to new location
                    board.Turtle.Move();

                    // Validate turtle location
                    board.ValidateTurtleLocation();
                }
            }
            catch (OutOfBoardException exception)
            {
                Console.WriteLine($"{exception.Message} | Location: [{exception.Location.X},{exception.Location.Y}]");
            }
            finally
            {
                inputMoves.Close();
            }

            Console.WriteLine("Hello World!");
        }
    }
}