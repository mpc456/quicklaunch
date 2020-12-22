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

namespace QuickLaunch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProcessRunner processRunner;

        private readonly IDictionary<string, ILaunchInformation> LaunchInformation;

        private List<KeyValuePair<string, ILaunchInformation>> CurrentMatchingEntries { get; set; }

        public MainWindow(IProcessRunner processRunner, IDataAccess dataAccess)
        {
            InitializeComponent();
            TextBox.Focus();
            this.processRunner = processRunner;
            LaunchInformation = dataAccess.GetLaunchInformation();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            var userInput = TextBox.Text;

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
            }

            if(matches.Count.Equals(1))
            {
                Instructions.Text = $"Press enter to go to '{matches.Single().Value.Name}'";
                Matches.Foreground = Brushes.Green;
            }

            if(matches.Count > 1)
            {
                Matches.Foreground = Brushes.Blue;
                Instructions.Text = "Continue typing";
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var userInput = TextBox.Text;

            if (e.Key == Key.Return)
            {
                e.Handled = true;

                if (userInput.Equals("exit") || userInput.Equals("quit"))
                    Application.Current.Shutdown();

                var matchedEntry = LaunchInformation.SingleOrDefault(i => i.Key.Contains(userInput));

                if (matchedEntry.Value != null)
                {
                    processRunner.Run(matchedEntry.Value);
                    this.WindowState = System.Windows.WindowState.Minimized;
                    TextBox.Text = string.Empty;
                    UpdateScreen();
                }
            }

            if(e.Key == Key.Escape)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
                TextBox.Text = string.Empty;
                UpdateScreen();
            }
        }


    }
}
