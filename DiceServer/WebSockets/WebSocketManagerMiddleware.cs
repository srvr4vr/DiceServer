using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DiceServer.WebSockets.Extensions;
using DiceServer.WebSockets.Handlers;
using Microsoft.AspNetCore.Http;

namespace DiceServer.WebSockets
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler WebSocketHandler { get; }

        public WebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            WebSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var connectionResult = await WebSocketHandler.OnConnected(socket, context);

            if(!connectionResult.Success)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, connectionResult.Message, CancellationToken.None);
                return;
            }

            await Receive(socket, async (result, message) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await WebSocketHandler.ReceiveAsync(context.GetUserId(), message);
                    return;
                }

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await WebSocketHandler.OnDisconnected(socket);
                }
            });

            _next?.Invoke(context);
        }

        private static async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, string> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                handleMessage(result, message);
            }
        }
    }
}
