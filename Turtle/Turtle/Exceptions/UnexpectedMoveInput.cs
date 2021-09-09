namespace Turtle.Exceptions
{
    using System;

    public class UnexpectedMoveInput : Exception
    {
        public string Input { get; }

        public UnexpectedMoveInput()
        {
        }

        public UnexpectedMoveInput(string message)
            : base(message)
        {
        }

        public UnexpectedMoveInput(string message, Exception inner)
            : base(message, inner)
        {
        }

        public UnexpectedMoveInput(string message, string input)
            : this(message)
        {
            this.Input = input;
        }
    }
}