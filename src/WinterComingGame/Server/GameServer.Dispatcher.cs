using WinterComingGame.Game;
using WinterComingGame.Game.Commands;

namespace WinterComingGame.Server
{
    partial class GameServer : IDispatcher
    {
        private readonly GameRoom _gameRoom = new GameRoom();

        public void Dispatch(IPlayer player, ICommand command)
        {
            command.Run(_gameRoom, player);
        }
    }
}
