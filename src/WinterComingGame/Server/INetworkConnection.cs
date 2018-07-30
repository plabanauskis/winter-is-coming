using System.Threading.Tasks;

namespace WinterComingGame.Server
{
    internal interface INetworkConnection
    {
        CommunicationChannel Channel { get; }
        Task ListenCommandsAsync(IDispatcher dispatcher);
    }
}