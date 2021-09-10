namespace Turtle
{
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.GameManagement;

    class Program
    {
        public static async Task Main(string[] args)
        {
            //var gameManager = new BasicGameManager();
            var gameManager = new AdvancedGameManager();

            await gameManager.Setup(new StreamReader(args[0]));

            await gameManager.GameLoop(new StreamReader(args[1]));
        }
    }
}