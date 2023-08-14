using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiceServer.WebSockets
{
    public class Broadcaster : IBroadcaster
    {
        private readonly WebSocketConnectionManager _connectionManager;

        public Broadcaster(WebSocketConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task Send(string userId, string message)
        {
            var ws = _connectionManager.GetSocketById(userId);

            var messageData = Encoding.UTF8.GetBytes(message);

            var buffer = new ArraySegment<byte>(messageData, 0, messageData.Length);
            await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}