namespace Turtle.GameObjects
{
    public class Empty : GameObject, IEmpty
    {
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