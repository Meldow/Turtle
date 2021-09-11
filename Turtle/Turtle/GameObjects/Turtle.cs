namespace Turtle.GameObjects
{
    using System;

    public class Turtle : GameObject, ITurtle
    {
        private DirectionEnum direction;

        public Turtle(IVector2 location, DirectionEnum direction)
        {
            this.Location = location;
            this.direction = direction;
        }

        public void Move()
        {
            switch (this.direction)
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

        public void Rotate()
        {
            switch (this.direction)
            {
                case DirectionEnum.North:
                    this.direction = DirectionEnum.East;
                    break;
                case DirectionEnum.East:
                    this.direction = DirectionEnum.South;
                    break;
                case DirectionEnum.South:
                    this.direction = DirectionEnum.West;
                    break;
                case DirectionEnum.West:
                    this.direction = DirectionEnum.North;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Rotate(DirectionEnum direction)
        {
            this.direction = direction;
        }

        public override char DrawCharacter()
        {
            return 'T';
        }
    }
}