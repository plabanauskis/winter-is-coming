using WinterComingGame.Game;
using Xunit;

namespace Tests
{
    public class ZombieTests
    {
        [Fact]
        public void ZombieIsAliveInitially()
        {
            var zombie = new Zombie();

            Assert.True(zombie.IsAlive);
        }

        [Fact]
        public void ZombieDiesAfterFiveHits()
        {
            var zombie = new Zombie();

            zombie.TakeHit();
            zombie.TakeHit();
            zombie.TakeHit();
            zombie.TakeHit();
            zombie.TakeHit();

            Assert.False(zombie.IsAlive);
        }

        [Fact]
        public void InitialZombieLocationIsZeroZero()
        {
            var zombie = new Zombie();

            Assert.Equal(new Coordinate(), zombie.Location);
        }

        [Fact]
        public void ZombieStaysInPlaceIfCannotMoveLeft()
        {
            var zombie = new Zombie();

            zombie.Move(MoveDirection.Left);

            Assert.Equal(new Coordinate(), zombie.Location);
        }

        [Fact]
        public void ZombieMovingRightIncresesXCoordinate()
        {
            var zombie = new Zombie();

            zombie.Move(MoveDirection.Right);

            Assert.Equal(new Coordinate(1, 0), zombie.Location);
        }

        [Fact]
        public void ZombieMovingLeftDecresesXCoordinate()
        {
            var zombie = new Zombie();
            zombie.Move(MoveDirection.Right);

            zombie.Move(MoveDirection.Left);

            Assert.Equal(new Coordinate(0, 0), zombie.Location);
        }

        [Fact]
        public void ZombieMovingTowardsWallIncreasesYCoordinate()
        {
            var zombie = new Zombie();

            zombie.Move(MoveDirection.TowardsWall);

            Assert.Equal(new Coordinate(0, 1), zombie.Location);
        }
    }
}
