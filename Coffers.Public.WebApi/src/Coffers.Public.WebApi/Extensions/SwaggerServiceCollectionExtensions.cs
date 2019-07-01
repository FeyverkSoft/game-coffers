using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Coffers.Public.WebApi.Extensions
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Coffer Public Api",
                    TermsOfService = "None"
                });
                
                options.CustomSchemaIds(type => type.FullName);

                options.MapType<Guid>(() => new Schema { Type = "string", Format = "uuid", Default = Guid.NewGuid() });
                options.DescribeAllEnumsAsStrings();

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(basePath, "Coffer.Public.WebApi.xml"));
            });
        }
    }
}

