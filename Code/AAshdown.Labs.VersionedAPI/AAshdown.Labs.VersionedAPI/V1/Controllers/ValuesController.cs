using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAshdown.Labs.VersionedAPI.V1.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Obsolete]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string[] Get()
        {
            return new[]
            {
                "One",
                "Two",
                "Three"
            };
        }
    }
}