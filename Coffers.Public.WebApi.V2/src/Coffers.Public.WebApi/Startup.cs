using System;
using System.Data;
using System.Net;
using System.Text.Json.Serialization;
using Asp.Core.FluentExtensions;
using Coffers.DB.Migrations;
using Coffers.Public.Domain.Authorization;
using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.Infrastructure.Authorization;
using Coffers.Public.Infrastructure.EmailSender;
using Coffers.Public.Infrastructure.UserRegistration;
using Coffers.Public.Queries.Infrastructure.Guilds;
using Coffers.Public.Queries.Infrastructure.Loans;
using Coffers.Public.Queries.Infrastructure.NestContracts;
using Coffers.Public.Queries.Infrastructure.Operations;
using Coffers.Public.Queries.Infrastructure.Penalties;
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
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Query.Core.FluentExtensions;
using Rabbita.Core.FluentExtensions;
using Rabbita.Entity.FluentExtensions;
using Rabbita.Entity.MariaDbTarget;
using Rabbita.InProc.FluentExtensions;

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
                .AddMvc(options => { options.Filters.Add(typeof(ErrorHandlingFilter)); })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<Infrastructure.Authorization.AuthorizationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });

            #region GuildCreate

            services.AddDbContext<Infrastructure.Admin.GuildCreate.GuildsDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.Admin.GuildCreate.IGuildRepository, Infrastructure.Admin.GuildCreate.GuildRepository>();
            services.AddScoped<Domain.Authorization.IAuthorizationRepository, Infrastructure.Authorization.AuthorizationRepository>();
            services.AddScoped<Domain.Admin.GuildCreate.GuildCreator>();

            #endregion

            #region UserRegistration

            services.AddDbContext<Infrastructure.UserRegistration.UserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.UserRegistration.IUserRegistrationRepository, Infrastructure.UserRegistration.UserRepository>();
            services.AddScoped<Domain.UserRegistration.UserRegistrarService>();

            #endregion

            #region Users

            services.AddDbContext<Infrastructure.Users.UserDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); });
            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.Users.UserRepository>();

            #endregion

            #region Guild

            #endregion

            #region Roles

            services.AddDbContext<Infrastructure.Roles.GuildsDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); });
            services.AddScoped<Domain.Roles.IGuildRepository, Infrastructure.Roles.GuildRepository>();

            #endregion

            #region Penalty

            services.AddDbContext<Infrastructure.Penalties.PenaltyDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); });
            services.AddScoped<Domain.Penalties.IPenaltyRepository, Infrastructure.Penalties.PenaltyRepository>();
            services.AddScoped<Domain.Penalties.IUserRepository, Infrastructure.Penalties.UserRepository>();
            services.AddScoped<Domain.Penalties.IOperationRepository, Infrastructure.Penalties.OperationRepository>();
            services.AddScoped<Domain.Penalties.PenaltyProcessor>();

            #endregion

            #region Loan

            services.AddDbContext<Infrastructure.Loans.LoanDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); });
            services.AddScoped<Domain.Loans.ILoanRepository, Infrastructure.Loans.LoanRepository>();
            services.AddScoped<Domain.Loans.IGuildRepository, Infrastructure.Loans.GuildRepository>();
            services.AddScoped<Domain.Loans.IOperationRepository, Infrastructure.Loans.OperationRepository>();
            services.AddScoped<Domain.Loans.LoanCreationService>();

            services.AddScoped<Domain.Loans.LoanExpireProcessor>();
            services.AddScoped<Domain.Loans.LoanTaxProcessor>();
            services.AddScoped<Domain.Loans.LoanProcessor>();

            #endregion

            #region Operations

            services.AddDbContext<Infrastructure.Operations.OperationDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); });
            services.AddScoped<Domain.Operations.IOperationsRepository, Infrastructure.Operations.OperationRepository>();
            services.AddScoped<Domain.Operations.IDocumentRepository, Infrastructure.Operations.DocumentRepository>();
            services.AddScoped<Domain.Operations.OperationCreator>();
            services.AddScoped<Domain.Operations.DocumentValidator>();
            services.AddScoped<Domain.Operations.DocumentSetter>();

            #endregion

            #region NestContracts

            services.AddDbContext<Infrastructure.NestContracts.NestContractDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Coffers"));
            });
            services.AddScoped<Domain.NestContracts.INestContractRepository, Infrastructure.NestContracts.NestContractRepository>();
            services.AddScoped<Domain.NestContracts.INestGetter, Infrastructure.NestContracts.NestContractRepository>();
            services.AddScoped<Domain.NestContracts.NestContractCreator>();

            #endregion

            services.AddScoped<Domain.Authorization.IPasswordHasher, PasswordHasher>();
            services.AddScoped<Domain.UserRegistration.IPasswordHasher, PasswordHasher>();
            services.AddScoped<IConfirmationCodeProvider, ConfirmationCodeProvider>();
            services.Configure<ConfirmationCodeProviderOptions>(Configuration.GetSection("ConfirmationCode"));

            services.AddScoped<UserSecurityService>();


            services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration.GetConnectionString("Coffers")));

            services.RegQueryProcessor(registry =>
            {
                registry.Register<GuildsQueryHandler>();
                registry.Register<UserQueryHandler>();
                registry.Register<OperationsQueryHandler>();
                registry.Register<LoanQueryHandler>();
                registry.Register<PenaltyQueryHandler>();
                registry.Register<NestContractQueryHandler>();
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

            #region Email

            services.Configure<Infrastructure.EmailSender.EmailSenderOptions>(Configuration.GetSection("EmailParams"));
            services.AddScoped<EmailSender>();

            #endregion

            services.AddHostedService<Infrastructure.Loans.LoanRecurrentProcessor>();
            services.AddHostedService<Infrastructure.NestContracts.ContractProcessor>();
            services.AddHostedService<Infrastructure.Penalties.PenaltyRecurrentProcessor>();
            services.AddSwagger();

            services.AddRabbitaDbPersistentMigrator(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("CoffersMigration");
                options.DbCommandTimeout = 30;
            });

            services
                .AddRabbitaSerializer()
                .AddRabbitaPersistent(
                    options => { },
                    options => { options.UseMySql(Configuration.GetConnectionString("Coffers")); }
                );

            services.AddEventBus();
            /*services.AddEventBus(registry =>
            {
                registry.AddEvent<Domain.Operations.Events.LoanOperationCreated>(_ =>
                {
                    _.Type = "LoanOperationCreated";
                    _.Queue = "Loan";
                    _.Exchanger = "WebApi";
                });
            });*/
            services.AddEventProcessor(registry =>
            {
                registry.Register<EventHandlers.LoanOperationCreatedEventHandler>();
                registry.Register<EventHandlers.PenaltyOperationCreatedEventHandler>();
                registry.Register<EventHandlers.ConfirmationCodeCreatedEventHandler>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
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
            app.UseAspNetCorePathBase();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Coffer Api v2"); });
            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (Int32) HttpStatusCode.Redirect));
        }
    }
}