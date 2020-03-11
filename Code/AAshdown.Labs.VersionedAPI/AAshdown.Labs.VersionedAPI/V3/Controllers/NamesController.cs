using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAshdown.Labs.VersionedAPI.V3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private static List<string> _names = new List<string>(new[]
        {
            "Adam",
            "Bob",
            "Claire",
            "Donna",
            "Eric"
        });

        [HttpGet]
        public string[] Get()
        {
            return _names.ToArray();
        }

        [HttpPost]
        public void Post(string newName)
        {
            if (!_names.Contains(newName))
            {
                _names.Add(newName);
            }
        }
    }
}