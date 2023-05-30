using System;

namespace SnakeGame
{
    /// <summary>
    /// class to control the direction of the snake, we assume that the upper left corner of the board is 0.0.
    /// </summary>
    public class Direction
    {
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);

        public int RowOffSet { get; }
        public int ColumnOffSet { get; }

        private Direction(int rowOffSet, int colorOffSet)
        {
            RowOffSet = rowOffSet;
            ColumnOffSet = colorOffSet;
        }

        public Direction Opposite()
        {
            return new Direction(-RowOffSet, -ColumnOffSet);
        }

        protected bool Equals(Direction other)
        {
            return RowOffSet == other.RowOffSet && ColumnOffSet == other.ColumnOffSet;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Direction) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffSet, ColumnOffSet);
        }

        public static bool operator ==(Direction? left, Direction? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Direction? left, Direction? right)
        {
            return !Equals(left, right);
        }
    }
}