namespace Turtle.GameManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Turtle.Exceptions;
    using Turtle.GameObjects;

    public class BasicGameManager : GameManager
    {
        public override async Task Setup(StreamReader inputGameSettings)
        {
            await this.CreateGameBoard(inputGameSettings);
            await this.CreateTurtle(inputGameSettings);
            await this.PopulateBoard(inputGameSettings);
        }

        public override async Task GameLoop(StreamReader inputMoves)
        {
            try
            {
                string readLine;
                while ((readLine = await inputMoves.ReadLineAsync()) != null)
                {
                    if (readLine == "m")
                    {
                        this.MoveTurtle();
                    }
                    else if (readLine == "r")
                    {
                        this.Turtle.Rotate();
                    }
                    else
                    {
                        throw new UnexpectedInputException(
                            "Unexpected move input, only 'm' and 'r' are acceptable.",
                            readLine);
                    }
                }
            }
            catch (OutOfBoardException exception)
            {
                Console.WriteLine($"{exception.Message} | Location: [{exception.Location.X},{exception.Location.Y}]");
            }
            catch (UnexpectedInputException exception)
            {
                Console.WriteLine($"{exception.Message} | Input: '{exception.Input}'");
            }
            finally
            {
                inputMoves.Close();
            }

            this.CheckGameStatus();
        }
    }
}