namespace Turtle.GameManagement
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IGameManager
    {
        Task Setup(StreamReader inputGameSettings);

        Task GameLoop(StreamReader inputMoves);
    }
}