namespace Turtle.GameObjects
{
    public class Exit : GameObject, IExit
    {
        public Exit(IVector2 location)
            : base(location)
        {
        }
    }
}