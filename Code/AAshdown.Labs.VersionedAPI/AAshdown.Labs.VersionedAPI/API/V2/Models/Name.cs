using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAshdown.Labs.VersionedAPI.API.V2.Models
{
    public class Name
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public Name(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
