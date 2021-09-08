namespace Turtle
{
    using System;

    public class Board2
    {
        public IGameObject[,] Tiles { get; set; }

        public Turtle Turtle { get; set; }

        public Board2(int xSize, int ySize)
        {
            this.Tiles = new IGameObject[xSize, ySize];
        }

        public void SetTurtleLocation(int x, int y)
        {
            this.Turtle.SetLocation(x, y);
        }

        public void AddMine(int x, int y)
        {
            this.Tiles[x, y] = new Mine(x, y);
        }

        public override string ToString()
        {
            Console.WriteLine("Game board:");
            Console.WriteLine($"{this.Tiles.Length}");
            return base.ToString();
        }
    }
}