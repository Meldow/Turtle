namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.Board;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class BasicGameManager : GameManager
    {
        public override async Task Setup(StreamReader inputGameSettings)
        {
            var boardSize = (await inputGameSettings.ReadLineAsync())?.Split(',');
            this.GameBoard = new GameBoard(int.Parse(boardSize[0]), int.Parse(boardSize[1]));

            var turtleLocation = (await inputGameSettings.ReadLineAsync())?.Split(',');
            this.Turtle = new Turtle(int.Parse(turtleLocation[0]), int.Parse(turtleLocation[1]), turtleLocation[2]);

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
                                this.GameBoard.AddGameObject(new Mine(new Vector2(locX, locY)));
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
                                this.GameBoard.AddGameObject(new Exit(new Vector2(locX, locY)));
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

        public override async Task GameLoop(StreamReader inputMoves)
        {
            try
            {
                string readLine;
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        this.Turtle.Move();

                        var obj = ValidateTurtleLocation(this.Turtle, this.GameBoard);

                        if (obj is Mine)
                        {
                            this.GameStatus = State.HitMine;
                            break;
                        }
                        else if (obj is Exit)
                        {
                            this.GameStatus = State.FoundExit;
                            break;
                        }
                    }
                    else if (readLine == "r")
                    {
                        this.Turtle.Rotate();
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

            this.CheckGameStatus();
        }
    }
}