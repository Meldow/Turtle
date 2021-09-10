namespace Turtle.GameObjects
{
    public class Mine : GameObject, IMine
    {
        public Mine(IVector2 location)
            : base(location)
        {
        }

        public override char DrawCharacter()
        {
            return 'M';
        }
    }
}