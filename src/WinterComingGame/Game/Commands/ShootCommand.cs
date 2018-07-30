using System;

namespace WinterComingGame.Game.Commands
{
    class ShootCommand : ICommand
    {
        private readonly Coordinate _shotCoordinate;

        public ShootCommand(Coordinate shotCoordinate)
        {
            _shotCoordinate = shotCoordinate;
        }

        public void Run(IGameRoom gameRoom, IPlayer player)
        {
            var game = gameRoom.FindGame(player);

            if (game == null)
            {
                throw new InvalidOperationException($"Player has not joined any game.");
            }

            game.Shoot(_shotCoordinate, player);
        }
    }
}
