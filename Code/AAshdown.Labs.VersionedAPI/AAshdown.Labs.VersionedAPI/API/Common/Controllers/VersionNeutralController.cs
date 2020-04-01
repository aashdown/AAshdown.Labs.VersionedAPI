using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAshdown.Labs.VersionedAPI.API.Common.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [ApiController]
    public class VersionNeutralController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello! I should exist in all versions of the API.";
        }
    }
}