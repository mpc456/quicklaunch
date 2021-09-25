using JetBrains.Annotations;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using QuickLaunch.Data.Access.File.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonFileDataAccess : IFileDataAccess
    {
        [NotNull] private readonly IDataAccessFileConfig _config;

        [NotNull] public string SupportedFileExtension => ".json";

        public JsonFileDataAccess([NotNull] IDataAccessFileConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
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
            throw new NotImplementedException();
        }
    }
}

