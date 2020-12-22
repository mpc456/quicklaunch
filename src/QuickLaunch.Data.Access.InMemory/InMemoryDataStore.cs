using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using QuickLaunch.Data.Access.Interface.DataModel;
using QuickLaunch.Data.Access.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickLaunch.Data.Access.InMemory
{
    public class InMemoryDataStore : IDataAccess
    {
        private readonly IDictionary<string, ILaunchInformation> LaunchInfo;
        private readonly ILogger<InMemoryDataStore> logger;

        public InMemoryDataStore([NotNull] ILogger<InMemoryDataStore> logger)
        {
            LaunchInfo = GetLauchInformation().ToDictionary(i => i.Name.ToLower());
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddLaunchInfo(ILaunchInformation info)
        {
            if(LaunchInfo.ContainsKey(info.Name))
            {
                logger.LogError($"key:{info.Name} already exists");
                return;
            }
            LaunchInfo.Add(info.Name,info);
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => LaunchInfo;

        public void UpdateLaunchInfo(ILaunchInformation info)
        {
            if (!LaunchInfo.ContainsKey(info.Name))
            {
                logger.LogError($"key:{info.Name} not found");
                return;
            }

            LaunchInfo[info.Name] = info;
        }

        private IEnumerable<ILaunchInformation> GetLauchInformation()
        {
            yield return new LaunchInformation { Name = "bbg", FileName = "https://www.bloomberg.com/" };
            yield return new LaunchInformation { Name = "reuters", FileName = "https://uk.reuters.com/" };
        }
    }
}
