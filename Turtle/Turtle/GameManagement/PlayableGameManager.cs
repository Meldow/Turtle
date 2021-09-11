namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class PlayableGameManager : GameManager
    {
        public override async Task Setup(StreamReader inputGameSettings)
        {
            await this.CreateGameBoard(inputGameSettings);
            await this.CreateTurtle(inputGameSettings);
            await this.PopulateBoard(inputGameSettings);
            AddEmptyTiles(this.GameBoard);
        }

        public override Task GameLoop(StreamReader inputMoves)
        {
            try
            {
                this.Draw();

                while (true)
                {
                    var command = Console.ReadKey();

                    switch (command.Key)
                    {
                        case ConsoleKey.UpArrow:
                            this.Turtle.Rotate(DirectionEnum.North);
                            this.Turtle.Move();
                            break;
                        case ConsoleKey.DownArrow:
                            this.Turtle.Rotate(DirectionEnum.South);
                            this.Turtle.Move();
                            break;
                        case ConsoleKey.LeftArrow:
                            this.Turtle.Rotate(DirectionEnum.West);
                            this.Turtle.Move();
                            break;
                        case ConsoleKey.RightArrow:
                            this.Turtle.Rotate(DirectionEnum.East);
                            this.Turtle.Move();
                            break;
                    }

                    this.Draw();

                    if (this.CheckCollisions())
                    {
                        break;
                    }
                }
            }
            catch (OutOfBoardException exception)
            {
                Console.WriteLine($"{exception.Message} | Location: [{exception.Location.X},{exception.Location.Y}]");
            }

            this.CheckGameStatus();
            return Task.CompletedTask;
        }

        private bool CheckCollisions()
        {
            var obj = ValidateTurtleLocation(this.Turtle, this.GameBoard);

            if (obj is Mine)
            {
                this.GameStatus = State.HitMine;
                return true;
            }
            else if (obj is Exit)
            {
                this.GameStatus = State.FoundExit;
                return true;
            }

            return false;
        }

        protected override async Task PopulateBoard(StreamReader inputGameSettings)
        {
            string readLine;
            while ((readLine = await inputGameSettings.ReadLineAsync()) != null)
            {
                try
                {
                    var input = readLine.Split(',');

                    switch (input[0])
                    {
                        case "m":
                            for (var i = 0; i < int.Parse(input[1]); i++)
                            {
                                var emptyTile = this.GameBoard.GetEmptyTile(this.Turtle.Location);
                                this.GameBoard.AddGameObject(new Mine(emptyTile));
                            }

                            break;

                        case "e":
                            for (var i = 0; i < int.Parse(input[1]); i++)
                            {
                                var emptyTile = this.GameBoard.GetEmptyTile(this.Turtle.Location);
                                this.GameBoard.AddGameObject(new Exit(emptyTile));
                            }

                            break;

                        case "a":
                            for (var i = 0; i < int.Parse(input[1]); i++)
                            {
                                var emptyTile = this.GameBoard.GetEmptyTile(this.Turtle.Location);
                                this.GameBoard.AddGameObject(new Apple(emptyTile));
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

        private void Draw()
        {
            Console.Clear();
            var strBuilder = new StringBuilder();

            for (var y = 0; y <= this.GameBoard.YSize; y++)
            {
                for (var x = 0; x <= this.GameBoard.XSize; x++)
                {
                    if (this.Turtle.Location.X == x && this.Turtle.Location.Y == y)
                    {
                        strBuilder.Append(this.Turtle.DrawCharacter());
                    }
                    else
                    {
                        strBuilder.Append(this.GameBoard.Tiles[x, y].DrawCharacter());
                    }
                }

                strBuilder.Append('\n');
            }

            Console.WriteLine(strBuilder);
        }
    }
}