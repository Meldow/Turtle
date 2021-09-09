namespace Turtle
{
    using System;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class Board2
    {
        private IGameObject[,] Tiles { get; set; }

        public Turtle Turtle { get; set; }

        private readonly int xSize;
        private readonly int ySize;

        public Board2(int xSize, int ySize)
        {
            this.Tiles = new IGameObject[xSize, ySize];
            this.xSize = xSize;
            this.ySize = ySize;
        }

        public void AddGameObject(IGameObject gameObject, int x, int y)
        {
            this.Tiles[x, y] = gameObject;
        }

        public IGameObject ValidateTurtleLocation()
        {
            if (this.Turtle.Location.X < 0 || this.Turtle.Location.X > this.xSize)
            {
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", this.Turtle.Location);
            }

            if (this.Turtle.Location.Y < 0 || this.Turtle.Location.Y > this.ySize)
            {
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", this.Turtle.Location);
            }

            return this.Tiles[this.Turtle.Location.X, this.Turtle.Location.Y];
        }

        public override string ToString()
        {
            Console.WriteLine("Game board:");
            Console.WriteLine($"{this.Tiles.Length}");
            return base.ToString();
        }
    }
}