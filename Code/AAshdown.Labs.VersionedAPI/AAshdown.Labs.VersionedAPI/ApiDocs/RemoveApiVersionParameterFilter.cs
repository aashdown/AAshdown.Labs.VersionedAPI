using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAshdown.Labs.VersionedAPI.ApiDocs
{
    public class RemoveApiVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameters = operation.Parameters.Where(p => p.Name == "api-version").ToArray();
            foreach(var param in versionParameters)
            {
                operation.Parameters.Remove(param);
            }
        }
    }
}
