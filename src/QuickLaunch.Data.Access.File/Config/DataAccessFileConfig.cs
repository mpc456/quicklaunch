using QuickLaunch.Data.Access.File.Interface.Config;
using System;

namespace QuickLaunch.Data.Access.File
{
    public class DataAccessFileConfig : IDataAccessFileConfig
    {
        public string FilePath { get; set; }

        public int NumberOfBackupsToKeep { get; set; }
    }
}
