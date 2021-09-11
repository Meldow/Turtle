namespace Turtle.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        public IVector2 Location { get; set; }

        protected GameObject()
        {
        }

        protected GameObject(IVector2 location) => this.Location = location;

        public abstract char DrawCharacter();
    }
}