using System;
using DiceServer.Login.Interfaces;
using DiceServer.WebSockets.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DiceServer.Login.Extensions;
using DiceServer.WebSockets.Invokers;
using DiceServer.WebSockets.Models;
using SuccessResponse = DiceServer.Models.SuccessResponse;

namespace DiceServer.WebSockets.Handlers
{
    public class WebSocketHandler
    {
        private readonly ITokenManager _tokenManager;
        private readonly IInvokerResolver _invokerResolver;
        private readonly WebSocketConnectionManager _webSocketConnectionManager;


        public WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager, ITokenManager tokenManager, IInvokerResolver invokerResolver)
        {
            _tokenManager = tokenManager;
            _invokerResolver = invokerResolver;
            _webSocketConnectionManager = webSocketConnectionManager;
        }

        public Task<SuccessResponse> OnConnected(WebSocket socket, HttpContext context)
        {
            SuccessResponse result;

            if (!IsTokenValid(context))
            {
                result = new SuccessResponse
                {
                    Success = false,
                    Message = "Connection fail. Wrong access token."
                };
            }
            else
            {
                _webSocketConnectionManager.AddSocket(socket, context);

                result = new SuccessResponse
                {
                    Success = true,
                    Message = string.Empty
                };
            }

            return Task.FromResult(result);
        }

        public async Task OnDisconnected(WebSocket socket)
        {
            await _webSocketConnectionManager.RemoveSocket(socket);
        }


        public async Task ReceiveAsync(string userId, string message)
        {
            var messageDto = message.FromJson<IncomingMessageDto>();

            var invoker = _invokerResolver.Resolve(messageDto);

            if (invoker != null)
            {
                await invoker.Invoke(userId, messageDto);
            }
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(
                    array: Encoding.ASCII.GetBytes(message),
                    offset: 0,
                    count: message.Length),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(_webSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var (_, socket) in _webSocketConnectionManager.GetAll())
            {
                if (socket.State == WebSocketState.Open)
                    await SendMessageAsync(socket, message);
            }
        }

        private bool IsTokenValid(HttpContext context) => 
            context.TryGetTokenGuid(out var guid) && 
            _tokenManager.IsValid(guid, context.GetUserId());
    }
}
