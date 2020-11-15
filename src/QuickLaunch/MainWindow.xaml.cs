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
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;

                if (CurrentTextBoxEntry.Equals("exit"))
                    Application.Current.Shutdown();

                if (LaunchInformation.ContainsKey(CurrentTextBoxEntry))
                {
                    processRunner.Run(LaunchInformation[CurrentTextBoxEntry]);
                    this.WindowState = System.Windows.WindowState.Minimized;
                }
            }
        }

}
}
