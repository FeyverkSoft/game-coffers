using System.Data;
using System.Net;
using System.Text.Json.Serialization;
using Coffers.DB.Migrations;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.Infrastructure.Authorization;
using Coffers.Public.Queries.Infrastructure.Guilds;
using Coffers.Public.Queries.Infrastructure.Users;
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
using MySql.Data.MySqlClient;
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
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });

            #region  GuildCreate

            services.AddDbContext<Infrastructure.Admin.GuildCreate.GuildsDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Admin.GuildCreate.IGuildRepository, Infrastructure.Admin.GuildCreate.GuildRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

            #endregion

            #region  UserRegistration

            services.AddDbContext<Infrastructure.UserRegistration.UserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.UserRegistration.IUserRepository, Infrastructure.UserRegistration.UserRepository>();
            services.AddScoped<UserFactory>();

            #endregion

            #region  UserRegistration

            services.AddDbContext<Infrastructure.Users.UserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.Users.UserRepository>();
            services.AddScoped<UserFactory>();

            #endregion

            #region Guild

            #endregion

            #region Roles
            services.AddDbContext<Infrastructure.Roles.GuildsDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Roles.IGuildRepository, Infrastructure.Roles.GuildRepository>();
            #endregion

            #region Penalty
            services.AddDbContext<Infrastructure.Penalties.PenaltyDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Penalties.IPenaltyRepository, Infrastructure.Penalties.PenaltyRepository>();
            services.AddScoped<Domain.Penalties.IUserRepository, Infrastructure.Penalties.UserRepository>();
            services.AddScoped<Domain.Penalties.IOperationRepository, Infrastructure.Penalties.OperationRepository>();
            services.AddScoped<Domain.Penalties.PenaltyProcessor>();
            #endregion

            #region Loan
            services.AddDbContext<Infrastructure.Loans.LoanDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Loans.ILoanRepository, Infrastructure.Loans.LoanRepository>();
            services.AddScoped<Domain.Loans.IGuildRepository, Infrastructure.Loans.GuildRepository>();
            services.AddScoped<Domain.Loans.IOperationRepository, Infrastructure.Loans.OperationRepository>();
            services.AddScoped<Domain.Loans.LoanCreationService>();

            services.AddScoped<Domain.Loans.LoanExpireProcessor>();
            services.AddScoped<Domain.Loans.LoanTaxProcessor>();
            services.AddScoped<Domain.Loans.LoanProcessor>();


            #endregion

            services.AddScoped<UserSecurityService>();

            services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration.GetConnectionString("Coffers")));

            services.RegQueryProcessor(registry =>
            {
                registry.Register<GuildsQueryHandler>();
                registry.Register<UserQueryHandler>();
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

            services.AddHostedService<Infrastructure.Loans.LoanRecurrentProcessor>();
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

            app.UseAuthentication();
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


