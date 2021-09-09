namespace Turtle.GameObjects
{
    using System;

    public class Turtle : GameObject
    {
        public DirectionEnum Direction;

        public Turtle(int x, int y, DirectionEnum direction)
            : base(x, y)
        {
            this.Direction = direction;
        }

        public void Move()
        {
            switch (Direction)
            {
                case DirectionEnum.North:
                    this.Location = new Vector2(this.Location.X, this.Location.Y - 1);
                    break;
                case DirectionEnum.East:
                    this.Location = new Vector2(this.Location.X + 1, this.Location.Y);
                    break;
                case DirectionEnum.South:
                    this.Location = new Vector2(this.Location.X, this.Location.Y + 1);
                    break;
                case DirectionEnum.West:
                    this.Location = new Vector2(this.Location.X - 1, this.Location.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Vector2 Forward()
        {
            switch (Direction)
            {
                case DirectionEnum.North:
                    return new Vector2(this.Location.X, this.Location.Y + 1);
                case DirectionEnum.East:
                    return new Vector2(this.Location.X + 1, this.Location.Y);
                case DirectionEnum.South:
                    return new Vector2(this.Location.X, this.Location.Y - 1);
                case DirectionEnum.West:
                    return new Vector2(this.Location.X - 1, this.Location.Y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Rotate()
        {
            switch (this.Direction)
            {
                case DirectionEnum.North:
                    this.Direction = DirectionEnum.East;
                    break;
                case DirectionEnum.East:
                    this.Direction = DirectionEnum.South;
                    break;
                case DirectionEnum.South:
                    this.Direction = DirectionEnum.West;
                    break;
                case DirectionEnum.West:
                    this.Direction = DirectionEnum.North;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}