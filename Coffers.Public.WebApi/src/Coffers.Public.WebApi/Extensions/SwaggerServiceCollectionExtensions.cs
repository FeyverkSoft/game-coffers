using System;
using System.Collections.Generic;
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
                options.IncludeXmlComments(Path.Combine(basePath, "Coffers.Public.Queries.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "Coffers.Types.xml"));

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "<p>Вставьте идентификатор сессии вместе c Bearer в поле. </p>" +
                                  "<p><b>Пример:</b> <br />" +
                                  "Authorization: Bearer 00000000-0000-4000-0000-000000000000</p>",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(new Dictionary<String, IEnumerable<String>>
                {
                    { "Bearer", new String[] { } }
                });
            });
        }
    }
}

