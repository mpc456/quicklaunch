using JetBrains.Annotations;

namespace QuickLaunch.Data.Access.File.Interface
{
    public interface IDataAccessFileConfig
    {
        [NotNull]
        string FilePath { get; }
    }
}