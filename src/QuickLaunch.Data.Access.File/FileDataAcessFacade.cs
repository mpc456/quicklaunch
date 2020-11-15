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

        public FileDataAcessFacade(IOptions<DataAccessFileConfig> config, IEnumerable<IFileDataAccess> dataAccessImplementations)
        {
            LoadData(config.Value);
            this.dataAccessImplementations = dataAccessImplementations;
        }

        private IDictionary<string, ILaunchInformation> LoadData(DataAccessFileConfig config)
        {
            var fileInfo = new FileInfo(config.FilePath);

            var fileDataAccess = dataAccessImplementations.Where(d => d.SupportedFileExtension.Equals(fileInfo.Extension));

            return fileDataAccess.First().GetLaunchInformation(config);
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => data;
    }
}
