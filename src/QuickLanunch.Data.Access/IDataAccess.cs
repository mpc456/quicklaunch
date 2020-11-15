using System.Collections.Generic;
using QuickLaunch.Data.Model;

namespace QuickLaunch.Services
{
    public interface IDataAccess
    {
        IDictionary<string, ILauchInformation> GetLaunchInformation();
    }
}
