using JetBrains.Annotations;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;
using System.Collections.Generic;

namespace QuickLaunch.Data.Access.File.Interface
{
    public interface IFileDataAccess
    {
        [NotNull]
        string SupportedFileExtension { get; }

        [NotNull]
        IDictionary<string, ILaunchInformation> ReadFromFile();

        void WriteToFile([NotNull] IDictionary<string, ILaunchInformation> info);
    }
}
