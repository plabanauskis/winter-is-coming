namespace WinterComingGame.Game.Commands
{
    interface ICommand
    {
        void Run(IGameRoom gameRoom, IPlayer player);
    }
}
