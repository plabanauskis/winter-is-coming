namespace WinterComingGame.Game.Commands
{
    class StartGameCommand : ICommand
    {
        public readonly string _gameName;

        public StartGameCommand(string gameName)
        {
            _gameName = gameName;
        }

        public void Run(IGameRoom gameRoom, IPlayer player) => gameRoom.StartGame(_gameName, player);
    }
}
