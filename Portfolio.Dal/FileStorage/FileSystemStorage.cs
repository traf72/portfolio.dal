using System.IO;
using System.Linq;
using Portfolio.Dal.Utils;

namespace Portfolio.Dal.FileStorage
{
    /// <summary>
    /// Реализация хранилища файлов в файловой системе 
    /// </summary>
    public class FileSystemStorage : IFileStorage
    {
        public string SaveFile(string containerId, string fileName, Stream data)
        {
            const string fileNameInStorageFormat = "{0}_{1}";
            var directoryPath = GetFullDirectoryPath(containerId);
            CreateDirectoryIfNeed(directoryPath);
            var filePrefix = UniqueGuidPartGenerator.Generate(4,
                prefix => File.Exists(string.Format(fileNameInStorageFormat, prefix, fileName)));
            var fileNameInStorage = string.Format(fileNameInStorageFormat, filePrefix, fileName);
            var filePath = Path.Combine(directoryPath, fileNameInStorage);
            using (var stream = File.Create(filePath))
            {
                data.CopyTo(stream);
            }

            return fileNameInStorage;
        }

        public Stream GetFile(FileInStorage file)
        {
            return File.OpenRead(Path.Combine(GetFullDirectoryPath(file.ContainerId), file.FileName));
        }

        public bool IsContainerExist(string containerId)
        {
            if (!Directory.Exists(Settings.StorageDirectory))
            {
                return false;
            }
            
            var directory = Directory.GetDirectories(Settings.StorageDirectory, containerId);
            return directory.Any();
        }

        public void DeleteFile(FileInStorage file)
        {
            var filePath = Path.Combine(GetFullDirectoryPath(file.ContainerId), file.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string CreateNewContainer()
        {
            var containerId = UniqueGuidPartGenerator.Generate(7, IsContainerExist);
            Directory.CreateDirectory(GetFullDirectoryPath(containerId));
            return containerId;
        }

        private static void CreateDirectoryIfNeed(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetFullDirectoryPath(string containerId)
        {
            var directoryPath = Path.Combine(Settings.StorageDirectory, containerId);
            return directoryPath;
        }
    }
}