using System;
using System.Net;
using Coffers.DB.Migrations;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Infrastructure.Authorization;
using Coffers.Public.Infrastructure.Gamers;
using Coffers.Public.Infrastructure.Guilds;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Extensions;
using Coffers.Public.WebApi.Filters;
using Coffers.Public.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Query.Core.FluentExtensions;

namespace Coffers.Public.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //add dependencies here
            services.AddMemoryCache();
            services.AddLogging();

            services.AddScoped<AuthorizationApiFilter>();

            //httpclient example
            //services.AddHttpClient<TClient>(client => client.BaseAddress = new Uri(Configuration["host"]))
            //    .AddHttpMessageHandler<RequestIdDelegatingHandler>()
            //    .AddHttpMessageHandler<LoggingDelegatingHandler>();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ErrorHandlingFilter));
                })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            ProcessDictionaryKeys = true
                        }
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<GuildsDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Coffers"));
            });
            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Coffers"));
            });
            services.AddDbContext<GamerDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Coffers"));
            });


            services.AddScoped<IGuildRepository, GuildRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<IGamerRepository, GamerRepository>();

            services.RegQueryProcessor(registry =>
            {
                registry.Register<GuildsQueryHandler>();
            });


            #region Auth

            services.AddAuthentication()
                .AddCookie("Coffers");

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Token";
                    options.DefaultScheme = "Token";
                    options.DefaultChallengeScheme = "Token";
                })
                .AddScheme<AuthenticationSchemeOptions, SessionAuthenticationHandler>("Token", "Token", o => { });
            #endregion

            #region Регион подключения проекта миграции
            services.AddDbContext<MigrateDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("Coffers")));
            services.AddHostedService<MigrateService<MigrateDbContext>>();

            #endregion


            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Coffers Public Api v1");
            });

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (Int32)HttpStatusCode.Redirect));
        }
    }
}


