using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using QuickLaunch.Services;
using QuickLaunch.Data.Access.Interface.Services;
using QuickLaunch.Data.Access.Interface.DataModel;
using Microsoft.Extensions.Logging;

namespace QuickLaunch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProcessRunner processRunner;
        private readonly ILogger<MainWindow> logger;
        private readonly IDictionary<string, ILaunchInformation> LaunchInformation;

        private List<KeyValuePair<string, ILaunchInformation>> CurrentMatchingEntries { get; set; }

        public MainWindow(IProcessRunner processRunner, 
            IDataAccess dataAccess, 
            ILogger<MainWindow> logger)
        {
            InitializeComponent();
            TextBox.Focus();
            this.processRunner = processRunner;
            this.logger = logger;
            logger.LogInformation("Starting");

            LaunchInformation = dataAccess.GetLaunchInformation();
            UpdateScreen();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            var userInput = TextBox.Text ?? string.Empty;

            if (userInput.Equals("exit") || userInput.Equals("quit"))
            {
                Instructions.Text = "Press enter to close application";
                return;
            }

            var matches = LaunchInformation.Where(i => i.Key.Contains(userInput)).ToList();

            Matches.Text = $"Matches: [{matches.Count}]";

            if (matches.Count.Equals(0))
            {
                Instructions.Text = "Enter a valid launch entry";
                Matches.Foreground = Brushes.Red;
                CurrentMatches.Text = string.Join(Environment.NewLine, LaunchInformation.Select(m => m.Key));
            }

            if(matches.Count.Equals(1))
            {
                Instructions.Text = $"Press enter to go to '{matches.Single().Value.Name}'";
                Matches.Foreground = Brushes.Green;
                CurrentMatches.Text = string.Join(Environment.NewLine, matches.Select(m => m.Key));
            }

            if(matches.Count > 1)
            {
                Matches.Foreground = Brushes.Blue;
                Instructions.Text = "Continue typing";
                CurrentMatches.Text = string.Join(Environment.NewLine, matches.Select(m => m.Key));
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var userInput = TextBox.Text;

            if (e.Key == Key.Return)
            {
                TextBox_KeyDown_Handle_Return(userInput);
                e.Handled = true;
            }

            if(e.Key == Key.Escape)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
                TextBox.Text = string.Empty;
            }

            UpdateScreen();

        }

        private void TextBox_KeyDown_Handle_Return(string userInput)
        {
            if (userInput.Equals("exit") || userInput.Equals("quit"))
                Application.Current.Shutdown();

            var matchedEntry = LaunchInformation.SingleOrDefault(i => i.Key.Contains(userInput));

            if (matchedEntry.Value != null)
            {
                processRunner.Run(matchedEntry.Value);
                this.WindowState = System.Windows.WindowState.Minimized;
                TextBox.Text = string.Empty;
                UpdateScreen();
                return;
            }
        }
    }
}
