using QuickLaunch.Data.Model;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.File.Strategies.Json
{
    public class JsonDataModel
    {
        public Dictionary<string, LaunchInformation> LaunchInformation { get; set; }
    }
}
