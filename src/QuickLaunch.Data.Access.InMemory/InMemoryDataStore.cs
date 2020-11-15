using QuickLaunch.Data.Access.Interface;
using QuickLaunch.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickLaunch.Data.Access.InMemory
{
    public class InMemoryDataStore : IDataAccess
    {
        private IDictionary<string, ILaunchInformation> LaunchInfo;

        public InMemoryDataStore()
        {
            LaunchInfo = GetLauchInformation().ToDictionary(i => i.Name.ToLower());
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => LaunchInfo;

        private IEnumerable<ILaunchInformation> GetLauchInformation()
        {
            yield return new LaunchInformation { Name = "bbc", FileName = "https://www.bbc.co.uk/" };
            yield return new LaunchInformation { Name = "reuters", FileName = "https://uk.reuters.com/" };
        }
    }
}
