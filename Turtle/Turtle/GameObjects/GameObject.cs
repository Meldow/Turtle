namespace Turtle.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        public IVector2 Location { get; set; }

        protected GameObject(int x, int y) => this.Location = new Vector2(x, y);
    }
}