using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jack.DataScience.Console;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MvcAngular.Generator;
using Serilog;
using Serilog.Events;

namespace VsExample.AspAPI
{
    public class Program
    {
        public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        public const string ASPNETCORE_URLS = "ASPNETCORE_URLS";
        public static void Main(string[] args)
        {
            if (AngularGenerator.ShouldRunMvc(args))
            {
                var logger = GetLogger();
                try
                {
                    logger.Information($"VsExample.AspAPI Startup: {string.Join(", ", args)}");

                    var desiredEnvironment = args.GetParameter("--env", "-e");
                    if (!string.IsNullOrWhiteSpace(desiredEnvironment))
                    {
                        Environment.SetEnvironmentVariable(ASPNETCORE_ENVIRONMENT, desiredEnvironment);
                    }

                    var desiredUrls = args.GetParameter("--urls", "-l");
                    if (!string.IsNullOrWhiteSpace(desiredUrls))
                    {
                        Environment.SetEnvironmentVariable(ASPNETCORE_URLS, desiredUrls);
                    }

                    var environment = Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT);
                    logger.Information($"VsExample.AspAPI ASPNETCORE_ENVIRONMENT: {environment}");

                    var urls = Environment.GetEnvironmentVariable(ASPNETCORE_URLS);
                    logger.Information($"VsExample.AspAPI ASPNETCORE_URLS: {urls}");

                    CreateWebHostBuilder(args).Build().Run();
                }
                catch(Exception ex)
                {
                    logger.Error(ex, "Error of MVC");
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        public static ILogger GetLogger()
        {
            if (loggerConfiguration == null || logger == null)
            {
                loggerConfiguration = new LoggerConfiguration();
                loggerConfiguration.MinimumLevel.Debug();
                LogEventLevel rollingFileEventLevel = LogEventLevel.Debug; // (LogEventLevel)Enum.Parse(typeof(LogEventLevel), options.RollingFileLogEventLevel);
                loggerConfiguration.WriteTo.RollingFile("logs/{Date}.txt", rollingFileEventLevel);
                LogEventLevel coloredConsoleEventLevel = LogEventLevel.Debug;  // (LogEventLevel)Enum.Parse(typeof(LogEventLevel), options.ConsoleLogEventLevel);
                loggerConfiguration.WriteTo.ColoredConsole(coloredConsoleEventLevel);
                logger = loggerConfiguration.CreateLogger();
            }
            return logger;
        }

        private static LoggerConfiguration loggerConfiguration;
        private static ILogger logger;

    }
}
