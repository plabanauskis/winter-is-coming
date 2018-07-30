using System.Threading.Tasks;

namespace WinterComingGame.Game
{
    interface IPlayer
    {
        Task NotifyOnVictoryAsync();
        Task NotifyOnGameOverAsync();
        Task NotifyOnLossAsync();
        Task NotifyOnZombieLocationAsync(Coordinate coordinate);
        Task NotifyOnShotResultAsync(string name, int result);
    }
}
