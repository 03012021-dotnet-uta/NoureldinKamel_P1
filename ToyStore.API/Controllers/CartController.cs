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


        [HttpGet("locationStacks/{id}")]
        public ActionResult<string> GetLocationInventories(Guid id)
        {
            System.Console.WriteLine("GetLocationInventories: " + id);
            List<SellableStack> stacks;
            if (_orderLogic.GetInventory(id, out stacks))
                return JsonConvert.SerializeObject(stacks,
                         new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            else return StatusCode(300);
        }

        [HttpPost("order")]
        public ActionResult<string> UpdateOrder(OrderModel orderModel)
        {
            System.Console.WriteLine("hellow??asdfasdfasfdasf");
            Customer customerOut;
            bool sdfs = _authenticator.InnerValidate(orderModel.token, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                System.Console.WriteLine("log orderModel:");
                System.Console.WriteLine(orderModel);
                if (_orderLogic.UpdateOrder(customerOut, orderModel))
                    return JsonConvert.SerializeObject(customerOut,
                             new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                else return StatusCode(401);
            }
            return StatusCode(401);
        }


        [HttpPost("checkout")]
        public ActionResult<string> CheckoutOrder([FromBody] Token token)
        {
            System.Console.WriteLine("hellow??asdfasdfasfdasf");
            Customer customerOut;
            bool sdfs = _authenticator.InnerValidate(token, out customerOut);
            if (sdfs == true)
            {
                if (_orderLogic.CheckoutCustomer(customerOut.CustomerId))
                    return JsonConvert.SerializeObject(customerOut,
                             new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                else return StatusCode(401);
            }
            return StatusCode(401);
        }


        [HttpDelete("order")]
        public ActionResult<string> DeleteOrder([FromBody] OrderDeleteModel odModel)
        {
            System.Console.WriteLine("hellow??asdfasdfasfdasf");
            Customer customerOut;
            bool sdfs = _authenticator.InnerValidate(odModel.token, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                System.Console.WriteLine("log orderModel:");
                System.Console.WriteLine(odModel);
                if (_orderLogic.DeleteStackFromOrder(customerOut, odModel))
                    return JsonConvert.SerializeObject(customerOut,
                             new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                else return StatusCode(401);
            }
            return StatusCode(401);
        }


        [HttpPatch("order")]
        public ActionResult<string> PatchOrder([FromBody] OrderPatchModel OrderPatchModel)
        {
            System.Console.WriteLine("hellow??asdfasdfasfdasf");
            Customer customerOut;
            bool sdfs = _authenticator.InnerValidate(OrderPatchModel.token, out customerOut);
            if (sdfs == true)
            {
                System.Console.WriteLine("token: " + customerOut.CustomerToken);
                System.Console.WriteLine("log orderModel:");
                System.Console.WriteLine(OrderPatchModel);
                if (_orderLogic.UpdateStackCountfromOrder(customerOut, OrderPatchModel))
                    return JsonConvert.SerializeObject(customerOut,
                             new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                else return StatusCode(401);
            }
            return StatusCode(401);
        }

    }
}
