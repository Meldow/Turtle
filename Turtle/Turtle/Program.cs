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

                if (input[0] == "m")
                {
                    board.AddMine(int.Parse(input[1]), int.Parse(input[2]));
                }

                if (input[0] == "e")
                {
                    board.AddExit(int.Parse(input[1]), int.Parse(input[2]));
                }
            }

            inputGameSettings.Close();

            var inputMoves = new StreamReader(args[1]);
            // Game Loop
            try
            {
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        board.Turtle.Move();

                        var obj = board.ValidateTurtleLocation();
                        if (obj is null)
                        {
                            continue;
                        }
                        else if (obj is Mine)
                        {
                            Console.WriteLine("Mine hit!");
                            break;
                        }
                        else if (obj is Exit)
                        {
                            Console.WriteLine("Turtle escaped successfully!");
                            break;
                        }
                    }
                    else if (readLine == "r")
                    {
                        board.Turtle.Rotate();
                        continue;
                    }

                    throw new UnexpectedMoveInput("Unexpected move input, only 'm' and 'r' are acceptable.", readLine);
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

            Console.WriteLine("Turtle did not manage to escape, still in danger!");
        }
    }
}