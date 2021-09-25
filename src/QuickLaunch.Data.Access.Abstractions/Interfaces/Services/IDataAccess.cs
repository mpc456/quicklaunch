using JetBrains.Annotations;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using System;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.Abstractions.Interfaces.Services
{
    public interface IDataAccess
    {
        [NotNull]
        IDictionary<string, ILaunchInformation> GetLaunchInformation();
        void AddLaunchInfo([NotNull] ILaunchInformation info);
        void UpdateLaunchInfo([NotNull] ILaunchInformation info);
    }
}
