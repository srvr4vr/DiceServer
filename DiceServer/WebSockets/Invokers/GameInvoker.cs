using System.Threading.Tasks;
using DiceServer.WebSockets.Models;

namespace DiceServer.WebSockets.Invokers
{
    public class GameInvoker : IInvoker
    {
        public bool IsApplicable(IncomingMessageDto message) => message.Type == "game";

        public Task Invoke(string userId, IncomingMessageDto message)
        {
            throw new System.NotImplementedException();
        }
    }
}