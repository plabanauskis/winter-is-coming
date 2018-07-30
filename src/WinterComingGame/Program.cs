using System.Threading.Tasks;
using WinterComingGame.Server;

namespace WinterComingGame
{
    class Program
    {
        static async Task Main(string[] args) => await GameServer.Instance.StartAsync();
    }
}
