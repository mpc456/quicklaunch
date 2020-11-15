using System.Collections.Generic;
using QuickLaunch.Model;

namespace QuickLaunch.Services
{
    public interface IDataAccess
    {
        IDictionary<string, ILauchInformation> GetLaunchInformation();
    }
}
