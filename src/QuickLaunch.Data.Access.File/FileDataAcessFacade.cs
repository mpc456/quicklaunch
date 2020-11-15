using Microsoft.Extensions.Options;
using QuickLaunch.Data.Access.Interface;
using QuickLaunch.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using QuickLaunch.Data.Access.File.Implementation;

namespace QuickLaunch.Data.Access.File
{
    /// <summary>
    /// Use filesystem to store application data
    /// </summary>
    public class FileDataAcessFacade : IDataAccess
    {
        private readonly IDictionary<string, ILaunchInformation> data;
        private readonly IEnumerable<IFileDataAccess> dataAccessImplementations;

        public FileDataAcessFacade(IDataAccessFileConfig config, IEnumerable<IFileDataAccess> dataAccessImplementations)
        {
            this.dataAccessImplementations = dataAccessImplementations;
            this.data = LoadData(config);
        }

        private IDictionary<string, ILaunchInformation> LoadData(IDataAccessFileConfig config)
        {
            var fileInfo = new FileInfo(config.FilePath);

            var fileDataAccess = dataAccessImplementations.Where(d => d.SupportedFileExtension.Equals(fileInfo.Extension));

            return fileDataAccess.First().GetLaunchInformation(config);
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => data;
    }
}
