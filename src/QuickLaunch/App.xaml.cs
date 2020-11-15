using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickLaunch.Data.Access.InMemory;
using QuickLaunch.Data.Access.Interface;
using QuickLaunch.Services;

namespace QuickLaunch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ServiceProvider;
        private IConfiguration Configuration;

        /// <summary>
        /// Entry point for application
        /// </summary>
        /// <param name="e">StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = CreateConfiguration();
            ServiceProvider = CreateServiceProvider();
            ShowMainWindow();
        }

        /// <summary>
        /// Create configuration from file
        /// </summary>
        /// <returns></returns>
        private IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
             .AddJsonFile("Config/appSettings.json", optional: false, reloadOnChange: true)
             .Build();
        }

        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProcessRunner, ProcessRunner>();
            services.AddTransient<IDataAccess, InMemoryDataStore>();
            services.AddTransient(typeof(MainWindow));
            return services.BuildServiceProvider();
        }

        private void ShowMainWindow()
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }


    }
}
