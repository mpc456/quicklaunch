using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Access.Interface.DataModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonFileDataAccess : IFileDataAccess
    {
        public string SupportedFileExtension => ".json";

        public IDictionary<string, ILaunchInformation> GetLaunchInformation(IDataAccessFileConfig config)
        {
            var jsonString = System.IO.File.ReadAllText(config.FilePath);
            var jsonContent = JsonSerializer.Deserialize<JsonDataModel>(jsonString);
            return jsonContent.LaunchInformation.Select(x => x as ILaunchInformation).ToDictionary(x => x.Name);
        }
    }
}

