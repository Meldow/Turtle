namespace Turtle
{
    public interface IGameObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        void SetLocation(int x, int y);
    }
}