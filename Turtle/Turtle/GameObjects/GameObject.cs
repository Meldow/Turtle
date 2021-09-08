namespace Turtle
{
    using System.Reflection.PortableExecutable;

    public abstract class GameObject : IGameObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        public GameObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void SetLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}