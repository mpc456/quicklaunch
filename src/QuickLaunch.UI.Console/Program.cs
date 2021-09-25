using QuickLaunch.Data.Access.Abstractions.Interfaces.Services;
using Spectre.Console;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using QuickLaunch.Operations.Abstractions.Interfaces.Services;
using System.Linq;

namespace QuickLaunch.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, ILaunchInformation> launchInformation = new Dictionary<string, ILaunchInformation>();
            IProcessRunner processRunner = null;

            AnsiConsole.Status()
                .Start("Starting...", ctx =>
                {
                    var bootStrapper = new BootStrapper();
                    var dataAccess = bootStrapper.ServiceProvider.GetRequiredService<IDataAccess>();
                    launchInformation = dataAccess.GetLaunchInformation();
                    processRunner = bootStrapper.ServiceProvider.GetRequiredService<IProcessRunner>();


                });

            AnsiConsole.MarkupLine($"Loaded {launchInformation.Count} links");

            var table = new Table().Centered();

            while(true)
            {
                string command = AnsiConsole.Ask<string>("Enter command:").Trim().ToLower();

                if (command.Equals("exit"))
                    break;

                if(command.Equals("list"))
                {
                    PrintLaunchInformation(launchInformation);
                    continue;
                }

                if (command.Equals("clear"))
                {
                    System.Console.Clear();
                    continue;
                }

                if (launchInformation.TryGetValue(command, out ILaunchInformation value))
                {
                    processRunner.Run(value);
                    continue;
                }

                var matches = launchInformation.Where(i => i.Key.Contains(command)).ToList();

                if (matches.Count.Equals(0))
                {
                    AnsiConsole.WriteLine($"Invalid entry");
                    continue;
                }

                if (matches.Count > 1)
                {
                    AnsiConsole.WriteLine($"{matches.Count} matches");
                    PrintLaunchInformation(matches);
                    continue;
                }

            }

            System.Console.ReadLine();

        }

        private static void PrintLaunchInformation(IEnumerable<KeyValuePair<string, ILaunchInformation>> launchInfo)
        {
            var table = new Table().LeftAligned();

            table.Title("Supported commands");
            table.AddColumn("Key");
            table.AddColumn("Path");
            table.AddColumn("Arguments");

            foreach(var info in launchInfo)
            {
                table.AddRow(info.Key, info.Value.FileName, info.Value.Arguments ?? string.Empty);
            }

            AnsiConsole.Render(table);
        }
    }
}
