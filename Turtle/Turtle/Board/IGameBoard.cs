namespace Turtle.Board
{
    using System.Collections.Generic;
    using Turtle.GameObjects;

    public interface IGameBoard
    {
        public int XSize { get; }

        public int YSize { get; }

        public IGameObject[,] Tiles { get; }

        void AddGameObject(IGameObject gameObject);

        IVector2 GetEmptyTile(IVector2 excludedVector = null);
    }
}