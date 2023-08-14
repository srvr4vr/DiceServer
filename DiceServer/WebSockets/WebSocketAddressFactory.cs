using DiceServer.Models;
using DiceServer.WebSockets.Extensions;
using Microsoft.Extensions.Configuration;

namespace DiceServer.WebSockets
{
    public class WebSocketAddressFactory : IWebSocketAddressFactory
    {
        private readonly IConfiguration _config;

        public WebSocketAddressFactory(IConfiguration config)
        {
            _config = config;
        }

        public string Create()
        {
            var hostString = GetHost();
            var pattern = _config.GetSection("WebSocketServerAddressPattern").Value;

            return string.Format(pattern, hostString);
        }
        
        private string GetHost()
        {
            var hostSettings = new Host();
            _config.GetSection("Host").Bind(hostSettings);

            var webSocketHostSettings = new Host();
            _config.GetSection("WsHost").Bind(webSocketHostSettings);

            hostSettings.Bind(webSocketHostSettings);

            if (hostSettings.Address == "*")
            {
                hostSettings.Address = DnsExtension.GetIp();
            }

            return $"{hostSettings.Address}:{hostSettings.Port}";
        }
    }
}