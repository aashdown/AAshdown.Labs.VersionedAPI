using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAshdown.Labs.VersionedAPI.ApiDocs
{
    public class AddApiVersionQueryStringParameterWithDefaultVersionFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach(var path in swaggerDoc.Paths)
            {
                path.Value.Parameters.Add(new OpenApiParameter()
                {
                    Name = "api-version",
                    Description = "API Version specifier (may also be provided as a header: API-VERSION)",
                    In = ParameterLocation.Query,
                    Schema = BuildApiVersionSchema(swaggerDoc.Info.Version),
                });
            }

            OpenApiSchema BuildApiVersionSchema(string apiVersion)
            {
                return new OpenApiSchema()
                {
                    Default = new OpenApiString(apiVersion)
                };
            }

        }
    }
}
