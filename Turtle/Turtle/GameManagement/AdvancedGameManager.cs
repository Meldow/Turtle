namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class AdvancedGameManager : GameManager
    {
        public override async Task Setup(StreamReader inputGameSettings)
        {
            await base.Setup(inputGameSettings);

            for (var x = 0; x <= this.GameBoard.XSize; x++)
            {
                for (var y = 0; y <= this.GameBoard.YSize; y++)
                {
                    this.GameBoard.Tiles[x, y] ??= new Empty(x, y);
                }
            }
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

                    this.Draw();
                    Thread.Sleep(1000);
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

            switch (this.GameStatus)
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