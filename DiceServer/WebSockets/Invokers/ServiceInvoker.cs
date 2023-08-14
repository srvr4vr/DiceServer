using System.Threading.Tasks;
using DiceServer.Login.Extensions;
using DiceServer.WebSockets.Models;

namespace DiceServer.WebSockets.Invokers
{
    public class ServiceInvoker : IInvoker
    {
        private readonly IBroadcaster _broadcaster;

        public ServiceInvoker(IBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public bool IsApplicable(IncomingMessageDto message) => message.Type == "service";

        public async Task Invoke(string userId, IncomingMessageDto message)
        {
            switch (message.Action)
            {
                case "ping":
                    var response = new SuccessResponse {Success = true, Message = "pong"};
                    await _broadcaster.Send(userId, response.ToJson());
                    break;
                case "pong":
                    //ToDo: complete ping-pong system if needed
                    break;
                default:
                    //Some other stuff
                    break;
            }
        }
    }
}