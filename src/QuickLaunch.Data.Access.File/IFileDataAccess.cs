using QuickLaunch.Data.Model;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.File.Implementation
{
    public interface IFileDataAccess
    {
        string SupportedFileExtension { get; }
        IDictionary<string, ILaunchInformation> GetLaunchInformation(IDataAccessFileConfig config);
    }
}
