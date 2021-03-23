using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        private readonly ILogger<ToyController> _logger;

        public ToyController(ILogger<ToyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        public IEnumerable<string> Get()
        {
            return new string[] { "hehe", "xd" };
        }
    }
}
