namespace Turtle.Board
{
    using Turtle.GameObjects;

    public interface IGameBoard
    {
        public int XSize { get; }

        public int YSize { get; }

        public IGameObject[,] Tiles { get; }

        void AddGameObject(IGameObject gameObject);

        void RemoveGameObject(IVector2 location);

        IVector2 GetEmptyTile(IVector2 excludedVector = null);
    }
}