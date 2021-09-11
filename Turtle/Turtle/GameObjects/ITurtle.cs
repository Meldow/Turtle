namespace Turtle.GameObjects
{
    public interface ITurtle : IGameObject
    {
        public int Apples { get; set; }

        void Move();

        void Rotate();

        void Rotate(DirectionEnum direction);
    }
}