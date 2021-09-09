namespace Turtle.GameObjects
{
    public class Vector2 : IVector2
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}