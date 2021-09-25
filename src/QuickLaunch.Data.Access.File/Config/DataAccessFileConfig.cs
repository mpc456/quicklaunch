using QuickLaunch.Data.Access.File.Interface;
using System;

namespace QuickLaunch.Data.Access.File
{
    public class DataAccessFileConfig : IDataAccessFileConfig
    {
        public string FilePath { get; set; }
    }
}
