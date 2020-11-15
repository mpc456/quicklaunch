using Microsoft.Extensions.Configuration;
using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.Strategies.Ini
{
    public class QrsFileDataAccess : IFileDataAccess
    {
        public string SupportedFileExtension => ".qrs";

        public IDictionary<string, ILaunchInformation> GetLaunchInformation(IDataAccessFileConfig config)
        {
            var configuration = new ConfigurationBuilder()
                .AddIniFile(config.FilePath)
                .Build();

            var data = new Dictionary<string, ILaunchInformation>();

            foreach (var entry in configuration.GetChildren())
            {
                var launchInformation = new LaunchInformation
                {
                    Name = entry.Path,
                    FileName = entry.GetSection("Filename").Value
                };

                data.Add(launchInformation.Name, launchInformation);
            }
            return data;
        }
    }
}
