namespace Turtle
{
    using System;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class Board2
    {
        public IGameObject[,] Tiles { get; set; }

        public Turtle Turtle { get; set; }

        private int xSize;
        private int ySize;

        public Board2(int xSize, int ySize)
        {
            this.Tiles = new IGameObject[xSize, ySize];
            this.xSize = xSize;
            this.ySize = ySize;
        }

        public void AddMine(int x, int y)
        {
            this.Tiles[x, y] = new Mine(x, y);
        }

        public IGameObject GetObject(Vector2 location)
        {
            return this.Tiles[location.X, location.Y];
        }

        public void ValidateTurtleLocation()
        {
            if (this.Turtle.Location.X < 0 || this.Turtle.Location.X > this.xSize)
            {
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", this.Turtle.Location);
            }

            if (this.Turtle.Location.Y < 0 || this.Turtle.Location.Y > this.ySize)
            {
                throw new OutOfBoardException("Invalid move, the Turtle dropped out of the board.", this.Turtle.Location);
            }

            var newPositionGameObject = this.Tiles[this.Turtle.Location.X, this.Turtle.Location.Y];

            if (newPositionGameObject == null)
            {
                return;
            }

            if (newPositionGameObject as Type == typeof(IMine))
            {
                Console.WriteLine("Mine hit!");
            }
        }

        public override string ToString()
        {
            Console.WriteLine("Game board:");
            Console.WriteLine($"{this.Tiles.Length}");
            return base.ToString();
        }
    }
}