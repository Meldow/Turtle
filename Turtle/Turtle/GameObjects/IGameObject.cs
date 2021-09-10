namespace Turtle.GameObjects
{
    public interface IGameObject
    {
        IVector2 Location { get; set; }

        char DrawCharacter();
    }
}