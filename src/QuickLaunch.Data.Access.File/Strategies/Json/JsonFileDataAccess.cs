using JetBrains.Annotations;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using QuickLaunch.Data.Access.Abstractions.Model;
using QuickLaunch.Data.Access.File.Interface;
using QuickLaunch.Data.Access.File.Interface.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonFileDataAccess : IFileDataAccess
    {
        [NotNull] private readonly IDataAccessFileConfig _config;
        [NotNull] private readonly IFileBackupService _backupService;

        [NotNull] public string SupportedFileExtension => ".json";

        public JsonFileDataAccess([NotNull] IDataAccessFileConfig config, 
            [NotNull] IFileBackupService backupService)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _backupService = backupService ?? throw new ArgumentNullException(nameof(backupService));
        }

        [NotNull]
        public IDictionary<string, ILaunchInformation> ReadFromFile()
        {
            var jsonString = System.IO.File.ReadAllText(_config.FilePath);
            var jsonContent = JsonSerializer.Deserialize<JsonDataModel>(jsonString);
            return jsonContent.LaunchInformation.Select(x => x as ILaunchInformation).ToDictionary(x => x.Name);
        }

        public void WriteToFile(IDictionary<string, ILaunchInformation> info)
        {
            var data = new JsonDataModel
            {
                LaunchInformation = info.Select(x => x.Value as LaunchInformation).ToList()
            };
            var jsonContent = JsonSerializer.Serialize<JsonDataModel>(data);
            _backupService.Write(Encoding.UTF8.GetBytes(jsonContent));
        }
    }
}

