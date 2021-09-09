namespace Turtle.Board
{
    using System;
    using Turtle.GameObjects;

    public class GameBoard : IGameBoard
    {
        public int XSize { get; }
        public int YSize { get; }
        public IGameObject[,] Tiles { get; }

        public GameBoard(int xSize, int ySize)
        {
            this.XSize = xSize;
            this.YSize = ySize;
            this.Tiles = new IGameObject[xSize, ySize];
        }

        public void AddGameObject(IGameObject gameObject, int x, int y)
        {
            this.Tiles[x, y] = gameObject;
        }

        public override string ToString()
        {
            Console.WriteLine("Game board:");
            Console.WriteLine($"{this.Tiles.Length}");
            return base.ToString();
        }
    }
}