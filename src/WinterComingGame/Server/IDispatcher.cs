using WinterComingGame.Game;
using WinterComingGame.Game.Commands;

namespace WinterComingGame.Server
{
    interface IDispatcher
    {
        void Dispatch(IPlayer player, ICommand command);
    }
}
