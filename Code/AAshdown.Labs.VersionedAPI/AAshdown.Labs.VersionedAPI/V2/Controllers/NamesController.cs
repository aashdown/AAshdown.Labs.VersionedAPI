using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAshdown.Labs.VersionedAPI.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        [HttpGet]
        public string[] Get()
        {
            return new[]
            {
                "Adam",
                "Bob",
                "Claire",
                "Donna",
                "Eric"
            };
        }
    }
}