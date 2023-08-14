using DiceServer.Database;
using DiceServer.Login.Interfaces;
using DiceServer.Models;
using DiceServer.WebSockets;
using Microsoft.Extensions.Configuration;

namespace DiceServer.Login.Implementations
{
    public class LoginService : ILogin
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IWebSocketAddressFactory _socketAddressFactory;
        private readonly string _salt;

        public LoginService(IConfiguration config, IUserRepository userRepository, ITokenManager tokenManager, IWebSocketAddressFactory socketAddressFactory)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
            _socketAddressFactory = socketAddressFactory;
            _salt = config.GetSection("Salt").Value;
        }

        public HelloResponse Login(string login, string password)
        {
            var result = new HelloResponse();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                result.Message = "Логин и пароль должны быть не пустыми";
            }
            else
            {
                var user = _userRepository.GetByName(login);

                if (user != null && Hash.Validate(password, _salt, user.Hash))
                {
                    result.Success = true;

                    var accessToken = _tokenManager.Create(user);

                    var socketAddress = _socketAddressFactory.Create();

                    result.UserId = user.Guid;
                    result.AccessToken = accessToken.ToString();

                    result.Message = $"Welcome home, Commander {user.Login}!";

                    result.Address = $"{socketAddress}?userId={{0}}&accessToken={{1}}";
                }
                else
                {
                    result.Message = "Неверный логин или пароль";
                }
            }

            return result;
        }
    }
}