namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.Board;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public abstract class GameManager : IGameManager
    {
        protected IGameBoard GameBoard;
        protected ITurtle Turtle;
        protected State GameStatus = State.Running;

        protected enum State
        {
            Running,
            FoundExit,
            HitMine,
        }

        public virtual async Task Setup(StreamReader inputGameSettings)
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

        public abstract Task GameLoop(StreamReader inputMoves);
    }
}