using DiceServer.Database;
using DiceServer.Login.Interfaces;
using DiceServer.Models;
using Microsoft.Extensions.Configuration;

namespace DiceServer.Login.Implementations
{
    public class Registrar : IRegistrar
    {
        private readonly IUserRepository _userRepository;
        private readonly string _salt;

        public Registrar(IConfiguration config, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _salt = config.GetSection("Salt").Value;
        }

        public SuccessResponse Register(string login, string email, string password)
        {
            var result = new SuccessResponse();

            if (_userRepository.GetByName(login) is null)
            {
                var hash = Hash.Create(password, _salt);

                result.Success = _userRepository.Add(login, email, hash);
                 
                result.Message = result.Success  
                    ?  "Регистрация успешна" 
                    :  "Непредвиденная ошибка";
            }
            else
            {
                result.Message = "Пользователь с таким именем уже существует";
            }

            return result;
        }
    }
}