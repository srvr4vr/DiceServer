using System.Threading.Tasks;
using DiceServer.WebSockets.Models;

namespace DiceServer.WebSockets.Invokers
{
    public interface IInvoker
    {
        bool IsApplicable(IncomingMessageDto message);
        Task Invoke(string userId, IncomingMessageDto message);
    }
}