using System;
using DiceServer.Login.Interfaces;
using DiceServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiceServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Hello : Controller
    {
        private readonly ILogin _loginService;

        public Hello(ILogin loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public ActionResult<HelloResponse> Get(string login, string password)
        {
            try
            {
                var loginResult = _loginService.Login(login, password);

                return Ok(loginResult);
            }
            catch(Exception ex)
            {
                return Ok(new HelloResponse{Message = ex.Message});
            }
        }
    }
}