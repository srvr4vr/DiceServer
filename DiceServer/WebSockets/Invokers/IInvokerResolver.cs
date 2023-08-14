using DiceServer.WebSockets.Models;

namespace DiceServer.WebSockets.Invokers
{
    public interface IInvokerResolver
    {
        IInvoker Resolve(IncomingMessageDto message);
    }
}