namespace Turtle.GameObjects
{
    public interface IGameObject
    {
        public IVector2 Location { get; set; }

        void SetLocation(int x, int y);
    }
}