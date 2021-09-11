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
            await this.CreateGameBoard(inputGameSettings);
            await this.CreateTurtle(inputGameSettings);
            await this.PopulateBoard(inputGameSettings);
            AddEmptyTiles(this.GameBoard);
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
                        this.MoveTurtle();
                    }
                    else if (readLine == "r")
                    {
                        this.Turtle.Rotate();
                    }
                    else
                    {
                        throw new UnexpectedInputException(
                            "Unexpected move input, only 'm' and 'r' are acceptable.",
                            readLine);
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

            this.CheckGameStatus();
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