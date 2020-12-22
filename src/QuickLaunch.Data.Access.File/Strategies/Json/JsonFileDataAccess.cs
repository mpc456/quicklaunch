using JetBrains.Annotations;
using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Access.Interface.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonFileDataAccess : IFileDataAccess
    {
        private readonly IDataAccessFileConfig config;

        private readonly IDictionary<string, ILaunchInformation> launchSettings;

        public string SupportedFileExtension => ".json";

        public JsonFileDataAccess([NotNull] IDataAccessFileConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IDictionary<string, ILaunchInformation> ReadFromFile()
        {
            var jsonString = System.IO.File.ReadAllText(config.FilePath);
            var jsonContent = JsonSerializer.Deserialize<JsonDataModel>(jsonString);
            return jsonContent.LaunchInformation.Select(x => x as ILaunchInformation).ToDictionary(x => x.Name);
        }

        public void WriteToFile(IDictionary<string, ILaunchInformation> info)
        {
            throw new System.NotImplementedException();
        }
    }
}

