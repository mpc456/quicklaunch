using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using QuickLaunch.Data.Access.File.DependencyInjection;
using QuickLaunch.Services;

namespace QuickLaunch
{
    /// <summary>
    /// Resolves configuration and dependency injection
    /// </summary>
    public class BootStrapper
    {
        [NotNull] public IServiceProvider ServiceProvider { get; }
        [NotNull] public IConfiguration Configuration { get; }

        public BootStrapper()
        {
            Configuration = CreateConfiguration();
            ServiceProvider = CreateServiceProvider();
        }

        /// <summary>
        /// Create configuration from file
        /// </summary>
        /// <returns></returns>
        private IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
             .AddJsonFile("Config/appSettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile("Config/nlogSettings.json", optional: false, reloadOnChange: true)
             .Build();
        }

        /// <summary>
        /// Setup dependency injection and create service provider
        /// </summary>
        /// <returns></returns>
        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProcessRunner, ProcessRunner>();
            services.AddFileDataAccess(Configuration);
            services.AddTransient(typeof(MainWindow));

            var nlogConfig = new NLogLoggingConfiguration(Configuration.GetSection("NLog"));

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(nlogConfig);
            });

            return services.BuildServiceProvider();
        }
    }
}
