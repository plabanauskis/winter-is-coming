namespace WinterComingGame.Game
{
    interface IGameRoom
    {
        void StartGame(string name, IPlayer player);
        Game FindGame(string name);
        Game FindGame(IPlayer player);
    }
}
