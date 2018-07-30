using System.Threading.Tasks;
using WinterComingGame.Game;
using WinterComingGame.Game.Commands;

namespace WinterComingGame.Server
{
    class Player : IPlayer, INetworkConnection
    {
        public Player(CommunicationChannel channel)
        {
            Channel = channel;
        }

        public async Task ListenCommandsAsync(IDispatcher dispatcher)
        {
            while (true)
            {
                try
                {
                    //var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));

                    var input = await Channel.ReadInputLineAsync(/*cts.Token*/);

                    var command = CommandBuilder.Build(input);

                    if (command == null)
                    {
                        await Channel.SendDataAsync("Unknown command");
                    }
                    else
                    {
                        dispatcher.Dispatch(this, command);
                    }
                }
                catch (TaskCanceledException)
                { }
            }
        }

        public CommunicationChannel Channel { get; }

        public Task NotifyOnVictoryAsync() => Channel.SendDataAsync("You killed the zombie!");

        public Task NotifyOnGameOverAsync() => Channel.SendDataAsync("Game over. Other player killed the zombie.");

        public Task NotifyOnLossAsync() => Channel.SendDataAsync("Zombie reached the wall!");

        public Task NotifyOnZombieLocationAsync(Coordinate coordinate) => Channel.SendDataAsync($"WALK night-king {coordinate.X} {coordinate.Y}");

        public Task NotifyOnShotResultAsync(string name, int result) => Channel.SendDataAsync($"BOOM {name} {result}");
    }
}
