using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using QuickLaunch.Data.Access.InMemory;

namespace QuickLaunch
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private BootStrapper BootStrapper;

        /// <summary>
        /// Entry point for application
        /// </summary>
        /// <param name="e">StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            BootStrapper = new BootStrapper();
            ShowMainWindow();
        }


        private void ShowMainWindow()
        {
            var mainWindow = BootStrapper.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }


    }
}
