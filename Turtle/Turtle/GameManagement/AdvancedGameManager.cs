namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
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
            // Draw Loop
            Console.Clear();

            // Generate Board
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

            Console.Read();
        }
    }
}