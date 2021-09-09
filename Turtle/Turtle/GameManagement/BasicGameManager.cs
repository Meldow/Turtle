namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.Board;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class BasicGameManager : IGameManager
    {
        private IGameBoard gameBoard;
        private ITurtle turtle;
        private State gameStatus = State.Running;

        private enum State
        {
            Running,
            FoundExit,
            HitMine,
        }

        public async Task Setup(StreamReader inputGameSettings)
        {
            var boardSize = (await inputGameSettings.ReadLineAsync())?.Split(',');
            this.gameBoard = new GameBoard(int.Parse(boardSize[0]), int.Parse(boardSize[1]));

            var turtleLocation = (await inputGameSettings.ReadLineAsync())?.Split(',');
            this.turtle = new Turtle(int.Parse(turtleLocation[0]), int.Parse(turtleLocation[1]), turtleLocation[2]);

            string readLine;
            while ((readLine = await inputGameSettings.ReadLineAsync()) != null)
            {
                try
                {
                    var input = readLine.Split(',');
                    var locX = int.Parse(input[1]);
                    var locY = int.Parse(input[2]);

                    switch (input[0])
                    {
                        case "m":
                            try
                            {
                                this.gameBoard.AddGameObject(new Mine(new Vector2(locX, locY)));
                            }
                            catch (OutOfBoardException exception)
                            {
                                Console.WriteLine(
                                    $"{exception.Message} Skipping this one. | Location: [{exception.Location.X},{exception.Location.Y}] , Object: [{exception.GameObject}]");
                            }

                            break;

                        case "e":
                            try
                            {
                                this.gameBoard.AddGameObject(new Exit(new Vector2(locX, locY)));
                            }
                            catch (OutOfBoardException exception)
                            {
                                Console.WriteLine(
                                    $"{exception.Message} Skipping this one. | Location: [{exception.Location.X},{exception.Location.Y}] , Object: [{exception.GameObject}]");
                            }

                            break;

                        default:
                            throw new UnexpectedInputException("Unexpected object input, only 'm' and 'e' are acceptable.", readLine);
                    }
                }
                catch (UnexpectedInputException exception)
                {
                    Console.WriteLine($"{exception.Message} Skipping this one. | Input: [{exception.Input}]");
                }
            }

            inputGameSettings.Close();
        }

        public async Task GameLoop(StreamReader inputMoves)
        {
            try
            {
                string readLine;
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        this.turtle.Move();

                        var obj = ValidateTurtleLocation(this.turtle, this.gameBoard);

                        if (obj is Mine)
                        {
                            this.gameStatus = State.HitMine;
                            break;
                        }
                        else if (obj is Exit)
                        {
                            this.gameStatus = State.FoundExit;
                            break;
                        }
                    }
                    else if (readLine == "r")
                    {
                        this.turtle.Rotate();
                    }
                    else
                    {
                        throw new UnexpectedInputException("Unexpected move input, only 'm' and 'r' are acceptable.", readLine);
                    }
                }
            }
            catch (OutOfBoardException exception)
            {
                Console.WriteLine($"{exception.Message} | Location: [{exception.Location.X},{exception.Location.Y}]");
            }
            catch (UnexpectedInputException exception)
            {
                Console.WriteLine($"{exception.Message} | Input: '{exception.Input}'");
            }
            finally
            {
                inputMoves.Close();
            }

            switch (this.gameStatus)
            {
                case State.Running:
                    Console.WriteLine("Turtle did not manage to escape, still in danger!");
                    break;
                case State.FoundExit:
                    Console.WriteLine("Turtle escaped successfully!");
                    break;
                case State.HitMine:
                    Console.WriteLine("Mine hit!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IGameObject ValidateTurtleLocation(ITurtle turtle, IGameBoard gameBoard)
        {
            if (turtle.Location.X < 0
                || turtle.Location.X > gameBoard.XSize
                || turtle.Location.Y < 0
                || turtle.Location.Y > gameBoard.YSize)
            {
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", turtle.Location);
            }

            return gameBoard.Tiles[turtle.Location.X, turtle.Location.Y];
        }
    }
}