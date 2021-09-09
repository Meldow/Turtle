namespace Turtle.Exceptions
{
    using System;

    public class UnexpectedInputException : Exception
    {
        public string Input { get; }

        public UnexpectedInputException()
        {
        }

        public UnexpectedInputException(string message)
            : base(message)
        {
        }

        public UnexpectedInputException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public UnexpectedInputException(string message, string input)
            : this(message)
        {
            this.Input = input;
        }
    }
}