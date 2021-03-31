using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToyStore.Business.Authentication;
using ToyStore.Business.Logic;
using ToyStore.Models.DBModels;
using ToyStore.Models.ControllerModels;

namespace ToyStore.API.Controllers
{
    /// <summary>
    /// this route is: weatherforecast
    /// it is not case sensitive
    /// when fetch is called fetch("weatherforecast")
    /// it comes into this method
    /// </summary>
    [ApiController]
    [Route("user/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Authenticator _authenticator;

        public AuthController(Authenticator _authenticator)
        {
            this._authenticator = _authenticator;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthModel authModel)
        {
            // System.Console.WriteLine("test: " + customer.CustomerUName + " " + customer.CustomerPass);
            Customer customerOut;
            var sdfs = _authenticator.Authenticate(authModel, out customerOut);
            if (sdfs)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                return JsonConvert.SerializeObject(customerOut,
                         new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            }
            return StatusCode(401);
        }


        [HttpPost("register")]
        public ActionResult<string> Register([FromBody] AuthModel authModel)
        {
            Customer customerOut;
            bool sdfs = _authenticator.CreateNewCustomer(authModel, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                return JsonConvert.SerializeObject(customerOut,
                         new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            }
            return StatusCode(401);
        }


        [HttpPost("validateToken")]
        public ActionResult<string> ValidateToken([FromBody] Token token)
        {
            Customer customerOut;
            bool sdfs = _authenticator.ValidateToken(token, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                return JsonConvert.SerializeObject(customerOut,
                         new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            }
            return StatusCode(401);
        }

    }
}
