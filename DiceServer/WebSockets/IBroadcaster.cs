using System.Threading.Tasks;

namespace DiceServer.WebSockets
{
    public interface IBroadcaster
    {
        Task Send(string userId, string message);
    }
}