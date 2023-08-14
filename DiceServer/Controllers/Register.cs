using System;
using DiceServer.Login.Interfaces;
using DiceServer.Models;
using Microsoft.AspNetCore.Mvc;


namespace DiceServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Register : Controller
    {
        private readonly IRegistrar _registrar;

        public Register(IRegistrar registrar)
        {
            _registrar = registrar;
        }

        [HttpPost]
        public ActionResult<SuccessResponse> Post([FromBody]RegisterRequestData data)
        {
            try
            {
                var result = _registrar.Register(data.Login, data.Email, data.Password);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(new SuccessResponse{Message = ex.Message});
            }
        }
    }
}