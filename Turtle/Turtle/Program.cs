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
            var gameBoard = await Setup(new StreamReader(args[0]));

            await GameLoop(new StreamReader(args[1]), gameBoard);
        }

        private static async Task GameLoop(StreamReader inputMoves, GameBoard gameBoard)
        {
            try
            {
                string readLine;
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        gameBoard.Turtle.Move();

                        var obj = gameBoard.ValidateTurtleLocation();

                        if (obj is Mine)
                        {
                            Console.WriteLine("Mine hit!");
                            break;
                        }
                        else if (obj is Exit)
                        {
                            gameBoard.Escaped = true;
                            Console.WriteLine("Turtle escaped successfully!");
                            break;
                        }
                    }
                    else if (readLine == "r")
                    {
                        gameBoard.Turtle.Rotate();
                    }
                    else
                    {
                        throw new UnexpectedMoveInput("Unexpected move input, only 'm' and 'r' are acceptable.", readLine);
                    }
                }

                if (!gameBoard.Escaped)
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

        private static async Task<GameBoard> Setup(StreamReader inputGameSettings)
        {
            var boardInput = await inputGameSettings.ReadLineAsync();
            var boardSize = boardInput.Split(',');
            var gameBoard = new GameBoard(int.Parse(boardSize[0]), int.Parse(boardSize[1]));

            var turtleLocationInput = await inputGameSettings.ReadLineAsync();
            var turtleLocation = turtleLocationInput.Split(',');

            gameBoard.Turtle = new Turtle(
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
                    gameBoard.AddGameObject(new Mine(locX, locY), locX, locY);
                }

                if (input[0] == "e")
                {
                    gameBoard.AddGameObject(new Exit(locX, locY), locX, locY);
                }
            }

            inputGameSettings.Close();
            return gameBoard;
        }
    }
}