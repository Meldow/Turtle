namespace Turtle.Board
{
    using System;
    using Turtle.Exceptions;
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
            this.Tiles = new IGameObject[xSize + 1, ySize + 1]; // Add +1 to size to take into account 0 based arrays
        }

        public void AddGameObject(IGameObject gameObject)
        {
            if (gameObject.Location.X < 0
                || gameObject.Location.X > this.XSize
                || gameObject.Location.Y < 0
                || gameObject.Location.Y > this.YSize)
            {
                throw new OutOfBoardException("Trying to add a GameObject out of the board.", gameObject.Location, gameObject);
            }

            this.Tiles[gameObject.Location.X, gameObject.Location.Y] = gameObject;
        }

        public override string ToString()
        {
            Console.WriteLine("Game board:");
            Console.WriteLine($"{this.Tiles.Length}");
            return base.ToString();
        }
    }
}