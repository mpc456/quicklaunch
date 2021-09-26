using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using QuickLaunch.Data.Access.File.Interface;
using QuickLaunch.Data.Access.File.Interface.Config;
using System;

using FileInfo = System.IO.FileInfo;
using Path = System.IO.Path;
using SystemFile = System.IO.File;

namespace QuickLaunch.Data.Access.File
{
    public class FileBackupService : IFileBackupService
    {
        [NotNull] private readonly IDataAccessFileConfig _config;
        [NotNull] private readonly ILogger<FileBackupService> _logger;
        [NotNull] private readonly FileInfo _fileStoreInfo;

        public FileBackupService([NotNull] IDataAccessFileConfig config,
            [NotNull] ILogger<FileBackupService> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileStoreInfo = new FileInfo(_config.FilePath);
        }

        public void Write(byte[] contents)
        {
            if (_config.NumberOfBackupsToKeep > 0)
                CreateBackups();

            SystemFile.WriteAllBytes(_config.FilePath, contents);
        }

        private void CreateBackups()
        {
            RemoveOldestAllowedBackup();
            BumpExistingBackupVersions();
            BackupCurrentFile();
        }

        private string GetBackupFileName(int version)
        {
            return Path.Combine(_fileStoreInfo.Directory.FullName, _config.FilePath, $".{version}.backup");
        }

        private void RemoveOldestAllowedBackup()
        {
            var oldestAllowedBackup = GetBackupFileName(_config.NumberOfBackupsToKeep);
            TryToDeleteFile(oldestAllowedBackup);
        }

        private void BumpExistingBackupVersions()
        {
            var numberOfBackupsToKeep = _config.NumberOfBackupsToKeep;
            var backupDirectory = _fileStoreInfo.Directory.FullName;

            for (int i = numberOfBackupsToKeep - 1; i > -1; i--)
            {
                var originalBackupFile = GetBackupFileName(i);
                var newBackupFile = GetBackupFileName(i + 1);
                TryToCopyFile(originalBackupFile, newBackupFile);
            }
        }

        private void BackupCurrentFile()
        {
            var currentFile = _fileStoreInfo.FullName;
            var backupFile = GetBackupFileName(1);
            TryToCopyFile(currentFile, backupFile);
        }

        private void TryToDeleteFile(string path)
        {
            try
            {
                if (!SystemFile.Exists(path))
                {
                    _logger.LogWarning($"File does not existing. {path}");
                    return;
                }

                _logger.LogInformation($"Deleting {path}");
                SystemFile.Delete(path);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to delete file {path}");
            }
        }

        private void TryToCopyFile(string source, string target)
        {
            try
            {
                if (!SystemFile.Exists(source))
                {
                    _logger.LogWarning($"File does not existing. {source}");
                    return;
                }
                _logger.LogInformation($"Copying file. Source={source}. Target={target}");
                SystemFile.Copy(source, target, true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to copy file. Source={source}. Target={target}");
            }
        }




    }
}
