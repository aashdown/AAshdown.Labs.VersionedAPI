using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAshdown.Labs.VersionedAPI.ApiDocs
{
    public static class SwaggerGenExtensions
    {
        public static void AddVersionedApiSupport(this IServiceCollection services, IConfiguration configuration)
        {
            var apiVersion = new Version(Convert.ToString(configuration["DefaultApiVersion"]));

            services.AddApiVersioning(o => {
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader(new string[] { "api-version" })
                );
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(apiVersion.Major, apiVersion.Minor);
                o.ReportApiVersions = true;
                o.Conventions.Add(new VersionByNamespaceConvention());
                o.UseApiBehavior = true; // include only api controller not mvc controller.
                o.ApiVersionSelector = new DefaultApiVersionSelector(o);
            });
            services.AddVersionedApiExplorer(o => {
                o.AddApiVersionParametersWhenVersionNeutral = false;
                o.DefaultApiVersion = new ApiVersion(apiVersion.Major, apiVersion.Minor);
                o.AssumeDefaultVersionWhenUnspecified = true;
            }); // It will be used to explorer api versioning and add custom text box in swagger to take version number.
            services.AddSwaggerGenerationUI();
        }
        public static void AddSwaggerGenerationUI(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider()
                             .GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(action =>
            {
                action.ResolveConflictingActions(x => x.First());
                action.OrderActionsBy(orderBy => orderBy.HttpMethod);
                //action.UseReferencedDefinitionsForEnums();
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    action.SwaggerDoc(item.GroupName, new OpenApiInfo
                    {
                        Title = $"Example API (v{item.GroupName})",
                        Version = item.ApiVersion.MajorVersion.ToString() + "." + item.ApiVersion.MinorVersion
                    }); ;

                    action.EnableAnnotations();

                    action.OperationFilter<RemoveApiVersionParameterFilter>();
                    action.DocumentFilter<AddApiVersionQueryStringParameterWithDefaultVersionFilter>();
                }
            });
        }

        public static void UseSwaggerGenerationUI(this IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IConfiguration configuration)
        {
            applicationBuilder.UseSwagger(c =>
            {
                c.RouteTemplate = "/apidoc/{documentname}/document.json";

                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers.Add(new OpenApiServer() { Url = "/api" }));
            });

            applicationBuilder.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "apidoc";
                c.DocumentTitle = "Api Help";
                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/apidoc/{item.GroupName}/document.json", item.GroupName);
                }
            });
        }
    }
}
