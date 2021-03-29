using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToyStore.Business.Logic;
using ToyStore.Models.DBModels;
using Newtonsoft.Json;

namespace ToyStore.API.Controllers
{
    /// <summary>
    /// this will be for retrieving 
    /// all toys
    /// specific toy in details
    /// toy being the product
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ToyController : ControllerBase
    {

        // private readonly ILogger<ToyController> _logger;

        // public ToyController(ILogger<ToyController> logger)
        // {
        //     _logger = logger;
        // }
        private readonly SellableLogic sellableLogic;

        public ToyController(SellableLogic sellableLogic)
        {
            this.sellableLogic = sellableLogic;
        }

        // [HttpGet]
        // // public IEnumerable<WeatherForecast> Get()
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "hehe", "xd" };
        // }


        // public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        // {
        //     var formatter = controllerSettings.Formatters.JsonFormatter;

        //     controllerSettings.Formatters.Remove(formatter);

        //     formatter = new JsonMediaTypeFormatter
        //     {
        //         SerializerSettings =
        //     {
        //         ContractResolver = new CamelCasePropertyNamesContractResolver()
        //     }
        //     };

        //     controllerSettings.Formatters.Insert(0, formatter);
        // }
        [HttpGet]
        public string Get()
        {
            // var options = new JsonSerializerOptions
            // {
            //     WriteIndented = true
            // };
            // var sellableStacks = sellableLogic.GetAllSellables();
            // var jsonString = JsonSerializer.Serialize(sellableStacks, options);
            // Console.WriteLine("json: " + jsonString);
            // sellableLogic.seedDB();
            var jsonString = sellableLogic.SerializeSellableStackList(sellableLogic.GetAllSellables());
            return JsonConvert.SerializeObject(sellableLogic.GetAllSellables(),
                new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            // return jsonString;
        }

        [HttpGet("tags")]
        public List<string> GetTags()
        {
            var nameList = new List<String>();
            sellableLogic.GetAvailableTags().ForEach(tag =>
            {
                System.Console.WriteLine(tag.TagName);
                nameList.Add(tag.TagName);
            });
            return nameList;
        }

        [HttpPost("detail")]
        public string GetProductDetail([FromBody] object obj)
        {
            System.Console.WriteLine("hello?: " + obj.ToString());
            var z = new { id = "" };
            string k = obj.ToString().Split("\"")[3];
            var id = new Guid(k);
            // var x = obj.GetType().GetProperty("id");
            // string y = (string)x.GetValue(obj, null);
            System.Console.WriteLine("hello?: " + k);
            // var oj = id.Property("id");
            var nameList = sellableLogic.GetSellableById(id);
            // var ser = Newtonsoft.Json.JsonSerializer.Create(
            // );

            return JsonConvert.SerializeObject(nameList,
                 new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            // return nameList;
            // return new SellableStack();

        }


        [HttpPost("customers")]
        public string GetCustomers([FromBody] object obj)
        {
            System.Console.WriteLine("hello?: " + obj.ToString());
            var z = new { id = "" };
            string k = obj.ToString().Split("\"")[3];
            var id = new Guid(k);
            // var x = obj.GetType().GetProperty("id");
            // string y = (string)x.GetValue(obj, null);
            System.Console.WriteLine("hello?: " + k);
            // var oj = id.Property("id");
            var nameList = sellableLogic.GetCustomersForStackWId(id);
            System.Console.WriteLine(nameList);
            // var ser = Newtonsoft.Json.JsonSerializer.Create(
            // );

            return JsonConvert.SerializeObject(nameList,
                 new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            // return nameList;
            // return new SellableStack();

        }
    }
}
