namespace WinterComingGame.Game
{
    class Zombie
    {
        private int _lives = 5;

        public Coordinate Location { get; set; }

        public void TakeHit() => _lives--;

        public void Move(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    if (Location.X > 0)
                    {
                        Location = Location.AddX(-1);
                    }
                    break;
                case MoveDirection.Right:
                    if (Location.X < 29)
                    {
                        Location = Location.AddX(1);
                    }
                    break;
                case MoveDirection.TowardsWall:
                    if (Location.Y < 29)
                    {
                        Location = Location.AddY(1);
                    }
                    break;
            }
        }

        public bool IsAlive => _lives > 0;
    }
}
