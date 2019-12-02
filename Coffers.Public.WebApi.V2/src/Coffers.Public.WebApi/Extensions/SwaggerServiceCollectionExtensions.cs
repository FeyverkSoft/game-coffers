using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Coffers.Public.WebApi.Extensions
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Coffer Public Api"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "<p>You need to set Bearer Token to get access to API</p>" +
                        "<p><b>Example:</b> <br />" +
                        "Authorization: Bearer b462e2b7-b3ba-42ca-8bae-963c56d21e18</p>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                };


                options.AddSecurityDefinition("Bearer", securityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                options.CustomSchemaIds(type => type.FullName);

                options.MapType<Guid>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "uuid",
                    Default = new OpenApiString(Guid.NewGuid().ToString())
                });
                options.DescribeAllEnumsAsStrings();

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(basePath, "Coffer.Public.WebApi.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "Coffers.Public.Queries.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "Coffers.Types.xml"));
            });
        }
    }
}

