using DiceServer.WebSockets.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DiceServer.WebSockets
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets =
            new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string token) => _sockets.FirstOrDefault(p => p.Key == token).Value;

        public ConcurrentDictionary<string, WebSocket> GetAll() => _sockets;

        public string GetId(WebSocket socket) => _sockets.FirstOrDefault(p => p.Value == socket).Key;

        public void AddSocket(WebSocket socket, HttpContext context)
        {
            _sockets.TryAdd(context.GetUserId(), socket);
        }

        public async Task RemoveSocket(string token)
        {
            _sockets.TryRemove(token, out var socket);

            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                "Closed by the WebSocketManager",
                CancellationToken.None);
        }

        public async Task RemoveSocket(WebSocket socket)
        {
            var token = _sockets.FirstOrDefault(p => p.Value == socket).Key;

            _sockets.TryRemove(token, out _);

            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                "Closed by the WebSocketManager",
                CancellationToken.None);
        }
    }
}