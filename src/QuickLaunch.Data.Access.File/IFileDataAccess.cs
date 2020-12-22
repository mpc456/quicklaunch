using JetBrains.Annotations;
using QuickLaunch.Data.Access.Interface.DataModel;
using QuickLaunch.Data.Access.Interface.Services;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.File.Implementation
{
    public interface IFileDataAccess
    {
        string SupportedFileExtension { get; }

        [NotNull]
        IDictionary<string, ILaunchInformation> ReadFromFile();

        void WriteToFile([NotNull] IDictionary<string, ILaunchInformation> info);
    }
}
