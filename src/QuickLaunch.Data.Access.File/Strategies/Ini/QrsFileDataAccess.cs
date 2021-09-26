using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using QuickLaunch.Data.Access.Abstractions.Model;
using QuickLaunch.Data.Access.File.Interface;
using QuickLaunch.Data.Access.File.Interface.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickLaunch.Data.Access.File.Strategies.Ini
{
    public class QrsFileDataAccess : IFileDataAccess
    {
        [NotNull] private readonly IDataAccessFileConfig _config;
        [NotNull] private readonly IFileBackupService _backupService;

        [NotNull] public string SupportedFileExtension => ".qrs";

        public QrsFileDataAccess([NotNull] IDataAccessFileConfig config, 
            [NotNull] IFileBackupService backupService)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _backupService = backupService ?? throw new ArgumentNullException(nameof(backupService));
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
            var content = new StringBuilder();
            foreach(var record in info)
            {
                content.AppendLine($"[{record.Key}]");
                content.AppendLine($"{nameof(record.Value.FileName)}=\"{record.Value.FileName}\"");
                content.AppendLine($"{nameof(record.Value.Arguments)}=\"{record.Value.Arguments}\"");
                content.AppendLine($"{nameof(record.Value.Notes)}=\"{record.Value.Notes}\"");
            }
            _backupService.Write(Encoding.UTF8.GetBytes(content.ToString()));
        }
    }
}
