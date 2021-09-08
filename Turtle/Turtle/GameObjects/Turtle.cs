namespace Turtle
{
    public class Turtle : GameObject
    {
        public DirectionEnum Direction;

        public Turtle(int x, int y, DirectionEnum direction)
            : base(x, y)
        {
            this.Direction = direction;
        }

    }
}