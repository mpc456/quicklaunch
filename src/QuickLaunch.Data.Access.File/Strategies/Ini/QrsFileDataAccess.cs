using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using QuickLaunch.Data.Access.Abstractions.Model;
using QuickLaunch.Data.Access.File.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.Strategies.Ini
{
    public class QrsFileDataAccess : IFileDataAccess
    {
        [NotNull] private readonly IDataAccessFileConfig _config;

        [NotNull] public string SupportedFileExtension => ".qrs";

        public QrsFileDataAccess([NotNull] IDataAccessFileConfig config)
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [NotNull]
        public IDictionary<string, ILaunchInformation> ReadFromFile()
        {
            var configuration = new ConfigurationBuilder()
                .AddIniFile(_config.FilePath)
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
