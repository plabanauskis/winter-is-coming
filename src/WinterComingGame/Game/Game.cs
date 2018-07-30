using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WinterComingGame.Game
{
    class Game
    {
        private readonly HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private readonly Timer _zombieMovementTimer;
        private readonly Random _movementRandomizer = new Random();
        private readonly Zombie _zombie = new Zombie();
        private bool _gameStarted;
        private bool _gameFinished;

        public Game(string name)
        {
            Name = name;
            _zombieMovementTimer = new Timer(MoveZombie, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }

        public string Name { get; }

        public IReadOnlyCollection<IPlayer> Players => _players;

        public void Shoot(Coordinate coordinate, IPlayer shootingPlayer)
        {
            if (coordinate == _zombie.Location)
            {
                _zombie.TakeHit();
                shootingPlayer.NotifyOnShotResultAsync(Name, 1);

                if (!_zombie.IsAlive)
                {
                    shootingPlayer.NotifyOnVictoryAsync();
                    foreach (var player in _players.Where(p => p != shootingPlayer))
                    {
                        player.NotifyOnGameOverAsync();
                    }
                }
            }
            else
            {
                shootingPlayer.NotifyOnShotResultAsync(Name, 0);
            }
        }

        public void Join(IPlayer player)
        {
            _players.Add(player);
        }

        private void MoveZombie(object state)
        {
            if (_gameFinished)
            {
                return;
            }

            if (!_gameStarted)
            {
                _gameStarted = true;
                return;
            }

            var moveDirection = GetMoveDirection();
            _zombie.Move(moveDirection);

            if (_zombie.Location.Y == 29)
            {
                foreach (var player in _players)
                {
                    player.NotifyOnLossAsync();
                }

                _gameFinished = true;
                return;
            }

            foreach (var player in _players)
            {
                player.NotifyOnZombieLocationAsync(_zombie.Location);
            }
        }

        private MoveDirection GetMoveDirection()
        {
            var moveDirection = (MoveDirection)_movementRandomizer.Next(0, 2);

            if (moveDirection == MoveDirection.Left &&
                _zombie.Location.X == 0)
            {
                moveDirection = MoveDirection.Right;
            }

            if (moveDirection == MoveDirection.Right &&
                _zombie.Location.X == 29)
            {
                moveDirection = MoveDirection.Left;
            }

            return moveDirection;
        }
    }
}
