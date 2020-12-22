using JetBrains.Annotations;
using QuickLaunch.Data.Access.Interface.DataModel;
using System;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.Interface.Services
{
    public interface IDataAccess
    {
        [NotNull]
        IDictionary<string, ILaunchInformation> GetLaunchInformation();
        void AddLaunchInfo([NotNull] ILaunchInformation info);
        void UpdateLaunchInfo([NotNull] ILaunchInformation info);
    }
}
