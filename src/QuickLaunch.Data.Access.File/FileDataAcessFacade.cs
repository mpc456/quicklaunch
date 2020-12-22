using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using QuickLaunch.Data.Access.File.Implementation;
using QuickLaunch.Data.Access.Interface.Services;
using QuickLaunch.Data.Access.Interface.DataModel;

namespace QuickLaunch.Data.Access.File
{
    /// <summary>
    /// Use filesystem to store application data
    /// </summary>
    public class FileDataAcessFacade : IDataAccess
    {
        private readonly IDictionary<string, ILaunchInformation> data;
        private readonly IDataAccessFileConfig config;
        private readonly IEnumerable<IFileDataAccess> dataAccessImplementations;
        private readonly IFileDataAccess fileDataAccess;

        public FileDataAcessFacade(IDataAccessFileConfig config, IEnumerable<IFileDataAccess> dataAccessImplementations)
        {
            this.config = config;
            this.dataAccessImplementations = dataAccessImplementations;
            this.fileDataAccess = GetDataAccess(config);
            this.data = fileDataAccess.ReadFromFile();
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => data;

        public void AddLaunchInfo(ILaunchInformation info)
        {
            if(data.ContainsKey(info.Name))
            {
                //Log error and return
            }
            data.Add(info.Name, info);
        }

        public void UpdateLaunchInfo(ILaunchInformation info)
        {
            if (!data.ContainsKey(info.Name))
            {
                //Log error and return
            }
            data[info.Name] = info;
        }


        private void SaveChanges()
        {
            fileDataAccess.WriteToFile(data);
        }

        private IFileDataAccess GetDataAccess(IDataAccessFileConfig config)
        {
            var fileInfo = new FileInfo(config.FilePath);
            var fileDataAccess = dataAccessImplementations.Where(d => d.SupportedFileExtension.Equals(fileInfo.Extension));
            return fileDataAccess.First();
        }
    }
}
