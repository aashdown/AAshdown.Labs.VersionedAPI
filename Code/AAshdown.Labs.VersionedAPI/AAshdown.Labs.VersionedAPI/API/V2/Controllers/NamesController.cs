using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAshdown.Labs.VersionedAPI.API.V2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAshdown.Labs.VersionedAPI.API.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private static List<Name> _names;

        private static List<Name> GetNames()
        {
            if(_names == null)
            {
                _names = new List<Name>();
                AddName("Adam");
                AddName("Bob");
                AddName("Claire");
                AddName("Donna");
                AddName("Eric");
            }

            return _names;
        }

        private static void AddName(string value)
        {
            _names.Add(new Name(_names.Count() + 1, value));
        }

        [HttpGet]
        public IEnumerable<Name> Get()
        {
            return GetNames();
        }

        [HttpPost]
        public void Post(string newName)
        {
            if (!GetNames().Any(n => n.Value == newName))
            {
                AddName(newName);
            }
        }
    }
}