using System;

namespace WinterComingGame.Game
{
    struct Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Coordinate AddX(int x) => new Coordinate(X + x, Y);
        public Coordinate AddY(int y) => new Coordinate(X, Y + y);

        public bool Equals(Coordinate other) => X == other.X && Y == other.Y;

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Coordinate other)
            {
                return Equals(other);
            }

            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode() => unchecked(X.GetHashCode() ^ 513 * Y.GetHashCode());

        public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
        public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);
    }
}
