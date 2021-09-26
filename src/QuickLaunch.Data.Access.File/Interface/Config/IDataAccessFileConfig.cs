using JetBrains.Annotations;

namespace QuickLaunch.Data.Access.File.Interface.Config
{
    public interface IDataAccessFileConfig
    {
        [NotNull]
        string FilePath { get; }

        int NumberOfBackupsToKeep { get; }
    }
}