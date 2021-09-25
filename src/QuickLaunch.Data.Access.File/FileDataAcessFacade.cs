using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using JetBrains.Annotations;
using QuickLaunch.Data.Access.File.Interface;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Services;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Model;

namespace QuickLaunch.Data.Access.File
{
    /// <summary>
    /// Use filesystem to store application data
    /// </summary>
    public class FileDataAcessFacade : IDataAccess
    {
        [NotNull] private readonly IDictionary<string, ILaunchInformation> _data;
        [NotNull] private readonly IDataAccessFileConfig _config;
        [NotNull] private readonly IEnumerable<IFileDataAccess> _implementations;
        [NotNull] private readonly ILogger<FileDataAcessFacade> _logger;
        [NotNull] private readonly IFileDataAccess _fileDataAccess;

        public FileDataAcessFacade([NotNull] IDataAccessFileConfig config,
            [NotNull] IEnumerable<IFileDataAccess> implementations,
            [NotNull] ILogger<FileDataAcessFacade> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _implementations = implementations ?? throw new ArgumentNullException(nameof(implementations));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileDataAccess = GetDataAccess(config);
            _data = _fileDataAccess.ReadFromFile();
        }

        public IDictionary<string, ILaunchInformation> GetLaunchInformation() => _data;

        public void AddLaunchInfo(ILaunchInformation info)
        {
            if(_data.ContainsKey(info.Name))
            {
                _logger.LogError($"key:{info.Name} already exists");
                return;
            }
            _data.Add(info.Name, info);
        }

        public void UpdateLaunchInfo(ILaunchInformation info)
        {
            if (!_data.ContainsKey(info.Name))
            {
                _logger.LogError($"key:{info.Name} not found");
                return;
            }
            _data[info.Name] = info;
        }


        private void SaveChanges()
        {
            _fileDataAccess.WriteToFile(_data);
        }

        private IFileDataAccess GetDataAccess(IDataAccessFileConfig config)
        {
            var fileInfo = new FileInfo(config.FilePath);
            var fileDataAccess = _implementations.Where(d => d.SupportedFileExtension.Equals(fileInfo.Extension));
            return fileDataAccess.First();
        }
    }
}
