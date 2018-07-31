using WinterComingGame.Game;
using Xunit;

namespace Tests
{
    public class CoordinateTests
    {
        [Fact]
        public void DefaultCoordinateIsZeroZero()
        {
            var coordinate = new Coordinate();

            Assert.Equal(0, coordinate.X);
            Assert.Equal(0, coordinate.Y);
        }

        [Fact]
        public void OldCoordinateRemainsUnaffectedAfterChange()
        {
            var coordinate = new Coordinate();

            coordinate.AddX(1);

            Assert.Equal(0, coordinate.X);
            Assert.Equal(0, coordinate.Y);
        }

        [Fact]
        public void XCoordinateCanBeIncreased()
        {
            var coordinate = new Coordinate();

            var newCoordinate = coordinate.AddX(1);

            Assert.Equal(1, newCoordinate.X);
        }

        [Fact]
        public void XCoordinateCanBeDecreased()
        {
            var coordinate = new Coordinate();

            var newCoordinate = coordinate.AddX(-1);

            Assert.Equal(-1, newCoordinate.X);
        }

        [Fact]
        public void YCoordinateCanBeIncreased()
        {
            var coordinate = new Coordinate();

            var newCoordinate = coordinate.AddY(1);

            Assert.Equal(1, newCoordinate.Y);
        }

        [Fact]
        public void YCoordinateCanBeDecreased()
        {
            var coordinate = new Coordinate();

            var newCoordinate = coordinate.AddY(-1);

            Assert.Equal(-1, newCoordinate.Y);
        }

        [Fact]
        public void IndependentCoordinatesCanBeEqual()
        {
            var a = new Coordinate(5, 6);
            var b = new Coordinate(5, 6);

            Assert.True(a == b);
            Assert.True(a.Equals(b));
            Assert.True(b.Equals(a));
            Assert.True(a.Equals((object)b));
        }

        [Fact]
        public void IndependentCoordinatesCanBeNotEqual()
        {
            var a = new Coordinate(5, 6);
            var b = new Coordinate(6, 5);

            Assert.False(a == b);
            Assert.False(a.Equals(b));
            Assert.False(b.Equals(a));
            Assert.False(a.Equals((object)b));
        }
    }
}
