namespace Turtle.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        public IVector2 Location { get; set; }


        public GameObject(int x, int y)
        {
            this.Location = new Vector2(x, y);
        }

        public void SetLocation(int x, int y)
        {
            this.Location = new Vector2(x, y);
        }
    }
}