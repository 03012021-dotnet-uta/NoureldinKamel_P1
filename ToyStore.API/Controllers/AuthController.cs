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

        public AuthController()
        {
            this._authenticator = Authenticator.Instance;
        }

        [HttpPost("login")]
        public string Login([FromBody] Customer customer)
        {
            System.Console.WriteLine("blz blz???");
            System.Console.WriteLine("test: " + customer.CustomerUName + " " + customer.CustomerPass);
            Customer customerOut;
            var sdfs = _authenticator.Authenticate(customer.CustomerUName, customer.CustomerPass, out customerOut);
            System.Console.WriteLine("token: " + customerOut.CustomerToken);
            return JsonConvert.SerializeObject(customerOut,
                     new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }


        [HttpPost("register")]
        public string Register([FromBody] Customer customer)
        {
            Customer customerOut;
            var sdfs = _authenticator.CreateNewCustomer(customer, out customerOut);
            System.Console.WriteLine("token: " + customerOut.CustomerToken);
            return JsonConvert.SerializeObject(customerOut,
                     new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }

    }
}
