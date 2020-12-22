using QuickLaunch.Data.Access.Interface.DataModel;
using System;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.Interface.Services
{
    public interface IDataAccess
    {
        IDictionary<string, ILaunchInformation> GetLaunchInformation();
    }
}
