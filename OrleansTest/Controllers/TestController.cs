using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTest.Grain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;

        public TestController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [HttpGet("{greeting}")]
        public async Task<ActionResult<string>> Get(string greeting)
        {
            var grain = _clusterClient.GetGrain<IHello>(0);
            var text = await grain.SayHello(greeting);
            return new ActionResult<string>(text);
        }
    }
}
