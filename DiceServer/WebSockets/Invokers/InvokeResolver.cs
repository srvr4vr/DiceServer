using System.Collections.Generic;
using System.Linq;
using DiceServer.WebSockets.Models;

namespace DiceServer.WebSockets.Invokers
{
    public class InvokerResolver : IInvokerResolver
    {
        private readonly IEnumerable<IInvoker> _invokers;

        public InvokerResolver(IEnumerable<IInvoker> invokers)
        {
            _invokers = invokers;
        }

        public IInvoker Resolve(IncomingMessageDto message) => message != null
            ? _invokers.FirstOrDefault(invoker => invoker.IsApplicable(message))
            : default;
    }
}