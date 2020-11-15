using System.Collections.Generic;
using System.Linq;
using QuickLaunch.Model;

namespace QuickLaunch.Services
{
    public class DataAccess : IDataAccess
    {
        private IDictionary<string, ILauchInformation> LaunchInfo;

        public DataAccess()
        {
            LaunchInfo = GetLauchInformation().ToDictionary(i => i.Name.ToLower());
        }

        public IDictionary<string, ILauchInformation> GetLaunchInformation() => LaunchInfo;

        private IEnumerable<ILauchInformation> GetLauchInformation()
        {
            yield return new LaunchInformation { Name = "bbc", FileName = "https://www.bbc.co.uk/" };
            yield return new LaunchInformation { Name = "reuters", FileName = "https://uk.reuters.com/" };
        }
    }
}
