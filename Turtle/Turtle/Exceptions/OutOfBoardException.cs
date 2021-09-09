namespace Turtle.Exceptions
{
    using System;
    using Turtle.GameObjects;

    public class OutOfBoardException : Exception
    {
        public IVector2 Location { get; }

        public OutOfBoardException()
        {
        }

        public OutOfBoardException(string message)
            : base(message)
        {
        }

        public OutOfBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public OutOfBoardException(string message, IVector2 location)
            : this(message)
        {
            this.Location = location;
        }
    }
}