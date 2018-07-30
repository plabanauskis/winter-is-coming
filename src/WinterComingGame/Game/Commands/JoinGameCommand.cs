using System;

namespace WinterComingGame.Game.Commands
{
    class JoinGameCommand : ICommand
    {
        private readonly string _gameName;

        public JoinGameCommand(string gameName)
        {
            _gameName = gameName;
        }

        public void Run(IGameRoom gameRoom, IPlayer player)
        {
            var game = gameRoom.FindGame(_gameName);

            if (game == null)
            {
                throw new InvalidOperationException($"Game '{_gameName}' does not exist.");
            }

            game.Join(player);
        }
    }
}
