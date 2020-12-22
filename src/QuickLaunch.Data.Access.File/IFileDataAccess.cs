using QuickLaunch.Data.Access.Interface.DataModel;
using QuickLaunch.Data.Access.Interface.Services;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.File.Implementation
{
    public interface IFileDataAccess
    {
        string SupportedFileExtension { get; }
        IDictionary<string, ILaunchInformation> ReadFromFile();
        void WriteToFile(IDictionary<string, ILaunchInformation> info);
    }
}
