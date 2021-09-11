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

        public abstract Task Setup(StreamReader inputGameSettings);

        public abstract Task GameLoop(StreamReader inputMoves);

        protected virtual async Task PopulateBoard(StreamReader inputGameSettings)
        {
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
                            throw new UnexpectedInputException(
                                "Unexpected object input, only 'm' and 'e' are acceptable.", readLine);
                    }
                }
                catch (UnexpectedInputException exception)
                {
                    Console.WriteLine($"{exception.Message} Skipping this one. | Input: [{exception.Input}]");
                }
            }
        }

        protected async Task CreateTurtle(StreamReader inputGameSettings)
        {
            var turtleLocation = (await inputGameSettings.ReadLineAsync())?.Split(',');
            var turtleLocationVec2 = new Vector2(int.Parse(turtleLocation[0]), int.Parse(turtleLocation[1]));
            var turtleDirection = Enum.Parse<DirectionEnum>(turtleLocation[2]);
            this.Turtle = new Turtle(turtleLocationVec2, turtleDirection);
        }

        protected async Task CreateGameBoard(StreamReader inputGameSettings)
        {
            var boardSize = (await inputGameSettings.ReadLineAsync())?.Split(',');
            this.GameBoard = new GameBoard(int.Parse(boardSize[0]), int.Parse(boardSize[1]));
        }

        protected IGameObject GetObjectInTurtleLocation(ITurtle turtle, IGameBoard gameBoard)
        {
            if (turtle.Location.X < 0
                || turtle.Location.X > gameBoard.XSize
                || turtle.Location.Y < 0
                || turtle.Location.Y > gameBoard.YSize)
            {
                SessionStatus.AddBoardDrop();
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", turtle.Location);
            }

            return gameBoard.Tiles[turtle.Location.X, turtle.Location.Y];
        }

        protected void CheckGameStatus()
        {
            switch (this.GameStatus)
            {
                case State.Running:
                    Console.WriteLine("Turtle did not manage to escape, still in danger!");
                    SessionStatus.AddTurtleLost();
                    break;
                case State.FoundExit:
                    Console.WriteLine("Turtle escaped successfully!");
                    SessionStatus.AddEscape();
                    break;
                case State.HitMine:
                    Console.WriteLine("Mine hit!");
                    SessionStatus.AddMineDeath();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected static void AddEmptyTiles(IGameBoard gameBoard)
        {
            for (var x = 0; x <= gameBoard.XSize; x++)
            {
                for (var y = 0; y <= gameBoard.YSize; y++)
                {
                    gameBoard.Tiles[x, y] ??= new Empty(new Vector2(x, y));
                }
            }
        }

        protected void MoveTurtle()
        {
            this.Turtle.Move();

            var obj = this.GetObjectInTurtleLocation(this.Turtle, this.GameBoard);

            if (obj is Mine)
            {
                this.GameStatus = State.HitMine;
            }
            else if (obj is Exit)
            {
                this.GameStatus = State.FoundExit;
            }
        }
    }
}