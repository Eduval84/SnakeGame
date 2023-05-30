using System;

namespace SnakeGame
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position Translate(Direction direction)
        {
            return new Position(Row + direction.RowOffSet , Column + direction.ColumnOffSet);
        }

        private bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !Equals(left, right);
        }
    }
}
