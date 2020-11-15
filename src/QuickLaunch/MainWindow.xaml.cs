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
using QuickLaunch.Model;

namespace QuickLaunch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProcessRunner processRunner;

        private string CurrentTextBoxEntry = string.Empty;
        private readonly IDictionary<string, ILauchInformation> LaunchInformation;
        private ILauchInformation CurrentMatch;

        public MainWindow(IProcessRunner processRunner, IDataAccess dataAccess)
        {
            InitializeComponent();
            TextBox.Focus();
            this.processRunner = processRunner;
            LaunchInformation = dataAccess.GetLaunchInformation();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            
            CurrentTextBoxEntry = textBox.Text.ToLower();

            if(CurrentTextBoxEntry.Equals("exit"))
            {
                TextBlock_Matches.Text = "Close application";
                return;
            }

            var matchingEntries = LaunchInformation.Where(i => i.Key.Contains(CurrentTextBoxEntry));

            if (!matchingEntries.Any())
            {
                TextBlock_Matches.Text = $"No matching entries";
                TextBlock_Matches.Foreground = Brushes.Red;
                TextBlock_Link.Text = string.Empty;
                CurrentMatch = null;
                return;
            }

            if(matchingEntries.Count().Equals(1))
            {
                CurrentMatch = matchingEntries.Single().Value;

                TextBlock_Matches.Text = $"Match: {CurrentMatch.Name}";
                TextBlock_Matches.Foreground = Brushes.Green;
                TextBlock_Link.Text = $"{CurrentMatch.FileName}";
                return;
            }

            TextBlock_Matches.Text = $"Matches: {matchingEntries.Count()}";
            TextBlock_Matches.Foreground = Brushes.Blue;
            TextBlock_Link.Text = string.Empty;
            CurrentMatch = null;
            return;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;

                if (CurrentTextBoxEntry.Equals("exit"))
                    Application.Current.Shutdown();

                if(CurrentMatch != null)
                {
                    processRunner.Run(CurrentMatch);
                    this.WindowState = System.Windows.WindowState.Minimized;
                }
            }
        }

}
}
