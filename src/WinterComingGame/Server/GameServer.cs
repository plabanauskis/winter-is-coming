using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WinterComingGame.Server
{
    partial class GameServer
    {
        public static GameServer Instance { get; } = new GameServer();

        private readonly SemaphoreSlim MaxConnectionsSemaphore = new SemaphoreSlim(100, 100);

        private TcpListener _tcpListener;
        private bool _listenerStarted;
        private ConcurrentDictionary<int, Task> _activeTasks = new ConcurrentDictionary<int, Task>();

        public async Task StartAsync()
        {
            _tcpListener = new TcpListener(IPAddress.Loopback, 44445);
            _tcpListener.Start();
            _listenerStarted = true;

            var waitForIncomingConnectionsTask = WaitForIncomingConnectionsAsync();
            _activeTasks.TryAdd(waitForIncomingConnectionsTask.Id, waitForIncomingConnectionsTask);

            while (_activeTasks.Values.Any(t => !t.IsCompleted))
            {
                var timedCancellationTask = TimeOutAsync();
                _activeTasks.TryAdd(timedCancellationTask.Id, timedCancellationTask);

                try
                {
                    var completedTask = await Task.WhenAny(_activeTasks.Values);
                    _activeTasks.Remove(completedTask.Id, out var _);
                }
                catch (OperationCanceledException)
                { }
            }
        }

        private async Task TimeOutAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));

            var cts = new CancellationTokenSource();
            cts.Cancel();
            await Task.FromCanceled(cts.Token);
        }

        public void Stop()
        {
            _listenerStarted = false;
            _tcpListener.Stop();
        }

        private async Task WaitForIncomingConnectionsAsync()
        {
            while (_listenerStarted)
            {
                try
                {
                    await MaxConnectionsSemaphore.WaitAsync();
                    var client = await _tcpListener.AcceptTcpClientAsync();
                    var communicationChannel = new CommunicationChannel(client);
                    var player = new Player(communicationChannel);
                    var listenCommandsTask = player.ListenCommandsAsync(this);
                    var x = _activeTasks.TryAdd(listenCommandsTask.Id, listenCommandsTask);
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }
    }
}
