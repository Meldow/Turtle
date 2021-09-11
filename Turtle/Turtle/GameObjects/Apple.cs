namespace Turtle.GameObjects
{
    public class Apple : GameObject, IMine
    {
        public Apple(IVector2 location)
            : base(location)
        {
        }

        public override char DrawCharacter()
        {
            return 'A';
        }
    }
}