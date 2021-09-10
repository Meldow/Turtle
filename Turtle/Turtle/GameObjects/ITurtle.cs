namespace Turtle.GameObjects
{
    public interface ITurtle : IGameObject
    {
        void Move();

        void Rotate();

        void Rotate(DirectionEnum direction);
    }
}