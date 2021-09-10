namespace Turtle.GameObjects
{
    public class Empty : GameObject, IEmpty
    {
        public Empty(int x, int y)
            : base(x, y)
        {
        }

        public Empty(IVector2 location)
            : base(location)
        {
        }

        public override char DrawCharacter()
        {
            return '-';
        }
    }
}