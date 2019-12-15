using System.Net;
using Coffers.DB.Migrations;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Infrastructure.Authorization;
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
using Query.Core.FluentExtensions;
using Coffers.LoanWorker;

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
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<GuildsDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });


            services.AddScoped<IGuildRepository, GuildRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();


            services.AddScoped<UserSecurityService>();
            services.AddScoped<LoanFactory>();
            services.AddScoped<OperationService>();


            services.RegQueryProcessor(registry =>
            {
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

            #region Включение миграции в проект
            services.AddDbContext<MigrateDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("CoffersMigration")));
            services.AddHostedService<MigrateService<MigrateDbContext>>();
            #endregion

            #region Включение воркера по займам в проект
            //пока что костыльно
            services.AddDbContext< LoanWorker.Infrastructure.LoanWorkerDbContext> (options =>
                options.UseMySql(Configuration.GetConnectionString("Coffers")));
            services.AddHostedService<LoanExpTaxService<LoanWorker.Infrastructure.LoanWorkerDbContext>>();
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
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Coffer Api v2"); });
            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (int)HttpStatusCode.Redirect));
        }
    }
}

