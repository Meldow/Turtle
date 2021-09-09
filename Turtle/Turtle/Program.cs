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
            var inputGameSettings = new StreamReader(args[0]);

            var boardInput = await inputGameSettings.ReadLineAsync();
            var boardSize = boardInput.Split(',');
            var board = new Board2(int.Parse(boardSize[0]), int.Parse(boardSize[1]));

            var turtleLocationInput = await inputGameSettings.ReadLineAsync();
            var turtleLocation = turtleLocationInput.Split(',');

            board.Turtle = new Turtle(
                int.Parse(turtleLocation[0]),
                int.Parse(turtleLocation[1]),
                Enum.Parse<DirectionEnum>(turtleLocation[2]));

            string readLine;
            while ((readLine = inputGameSettings.ReadLine()) != null)
            {
                var input = readLine.Split(',');
                var locX = int.Parse(input[1]);
                var locY = int.Parse(input[2]);

                if (input[0] == "m")
                {
                    board.AddGameObject(new Mine(locX, locY), locX, locY);
                }

                if (input[0] == "e")
                {
                    board.AddGameObject(new Exit(locX, locY), locX, locY);
                }
            }

            inputGameSettings.Close();

            // Game Loop
            var inputMoves = new StreamReader(args[1]);
            try
            {
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        board.Turtle.Move();

                        var obj = board.ValidateTurtleLocation();

                        if (obj is Mine)
                        {
                            Console.WriteLine("Mine hit!");
                            break;
                        }
                        else if (obj is Exit)
                        {
                            board.Escaped = true;
                            Console.WriteLine("Turtle escaped successfully!");
                            break;
                        }
                    }
                    else if (readLine == "r")
                    {
                        board.Turtle.Rotate();
                    }
                    else
                    {
                        throw new UnexpectedMoveInput("Unexpected move input, only 'm' and 'r' are acceptable.", readLine);
                    }
                }

                if (!board.Escaped)
                {
                    Console.WriteLine("Turtle did not manage to escape, still in danger!");
                }
            }
            catch (OutOfBoardException exception)
            {
                Console.WriteLine($"{exception.Message} | Location: [{exception.Location.X},{exception.Location.Y}]");
            }
            catch (UnexpectedMoveInput exception)
            {
                Console.WriteLine($"{exception.Message} | Input: '{exception.Input}'");
            }
            finally
            {
                inputMoves.Close();
            }
        }
    }
}