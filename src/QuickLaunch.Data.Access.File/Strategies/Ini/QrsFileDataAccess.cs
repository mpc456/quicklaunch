using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Access.Interface.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.Strategies.Ini
{
    public class QrsFileDataAccess : IFileDataAccess
    {
        private readonly IDataAccessFileConfig config;

        public string SupportedFileExtension => ".qrs";

        public QrsFileDataAccess([NotNull] IDataAccessFileConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IDictionary<string, ILaunchInformation> ReadFromFile()
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

        public void WriteToFile(IDictionary<string, ILaunchInformation> info)
        {
            throw new NotImplementedException();
        }
    }
}
