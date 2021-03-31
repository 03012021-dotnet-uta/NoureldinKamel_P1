using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToyStore.Business.Authentication;
using ToyStore.Business.Logic;
using ToyStore.Models.ControllerModels;
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
    public class CartController : ControllerBase
    {
        private readonly Authenticator _authenticator;
        private readonly OrderLogic _orderLogic;

        public CartController(Authenticator _authenticator, OrderLogic _orderLogic)
        {
            this._authenticator = _authenticator;
            this._orderLogic = _orderLogic;
        }

        [HttpPost("order")]
        public ActionResult<string> UpdateOrder([FromBody] OrderModel orderModel)
        {
            Customer customerOut;
            bool sdfs = _authenticator.InnerValidate(orderModel.token, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                System.Console.WriteLine("log orderModel:");
                System.Console.WriteLine(orderModel);
                _orderLogic.UpdateOrder(customerOut, orderModel);
                return JsonConvert.SerializeObject(customerOut,
                         new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            }
            return StatusCode(401);
        }


    }
}
