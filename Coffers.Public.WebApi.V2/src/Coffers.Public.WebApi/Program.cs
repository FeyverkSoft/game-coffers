using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Rabbita.Entity.Migration;

namespace Coffers.Public.WebApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(String[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .MessagingDbInitialize()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(String[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration);
                }).ConfigureLogging(_ =>
                {
                    _.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Trace);
                    _.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Trace);
                    _.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Trace);
                });
    }
}