using QuickLaunch.Data.Model;
using System;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.Interface
{
    public interface IDataAccess
    {
        IDictionary<string, ILauchInformation> GetLaunchInformation();
    }
}
