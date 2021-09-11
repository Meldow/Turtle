namespace Turtle
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.GameManagement;

    /*
     * Basic
     * Receives a game-settings with position of all objects
     * Receives a moves with all movements
     * Only returns the final state
     *
     * how to run:
     * dotnet run b game-settings.in moves.in
     *
     * Advanced
     * Receives a game.settings with position of all objects
     * Receives a moves with all movements
     * Draws a UI with Turtle movements
     *
     * how to run:
     * dotnet run a game-settings.in moves.in
     *
     * Playable
     * Allows you to control the turtle with arrows keys
     *
     * how to run:
     * dotnet run p playable-game-settings.in moves.in
     */
    class Program
    {
        public static async Task Main(string[] args)
        {
            GameManager gameManager = null;
            while (true)
            {
                switch (args[0])
                {
                    case "basic":
                    case "b":
                        gameManager = new BasicGameManager();
                        break;
                    case "advanced":
                    case "a":
                        gameManager = new AdvancedGameManager();
                        break;
                    case "playable":
                    case "p":
                        gameManager = new PlayableGameManager();
                        break;
                    default:
                        Console.WriteLine(
                            "Unknown game type. Please use basic (b), advanced (a) or playable (p) as the first argument.");
                        Environment.Exit(0);
                        break;
                }

                var gameSettingsStreamReader = new StreamReader(args[1]);
                await gameManager.Setup(gameSettingsStreamReader);
                gameSettingsStreamReader.Close();

                var movesStreamReader = new StreamReader(args[2]);
                await gameManager.GameLoop(movesStreamReader);
                movesStreamReader.Close();

                SessionStatus.Draw();
                Console.WriteLine("Restarting game in 5...");
                await Task.Delay(5000);
                Console.Clear();
            }
        }
    }
}