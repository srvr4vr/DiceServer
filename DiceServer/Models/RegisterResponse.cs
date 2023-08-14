using System.Security.Policy;

namespace DiceServer.Models
{
    public class RegisterResponse : SuccessResponse
    {
        public string UserTo { get; set; }
    }
}