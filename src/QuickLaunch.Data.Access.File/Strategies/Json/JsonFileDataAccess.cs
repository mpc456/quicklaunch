using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonFileDataAccess : IFileDataAccess
    {
        public string SupportedFileExtension => "json";

        public IDictionary<string, ILauchInformation> GetLaunchInformation(DataAccessFileConfig config)
        {
            var jsonString = System.IO.File.ReadAllText(config.FilePath);
            var jsonContent = JsonSerializer.Deserialize<JsonDataModel>(jsonString);
            return jsonContent.LaunchInformation as IDictionary<string, ILauchInformation>;
        }
    }
}
