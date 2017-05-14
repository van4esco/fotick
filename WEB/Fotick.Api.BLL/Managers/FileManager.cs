using System;
using System.Collections.Generic;
using System.Text;

namespace Fotick.Api.BLL.Managers
{
    public class FileManager
    {
        private readonly string _userFilesPath;
        private readonly string _fileBasePath;

        public FileManager()
        {
            _fileBasePath = HostingEnvironment.MapPath("~/Files/Images");
        }

        public string UploadFile(HttpPostedFileBase file, string pathOfFolder, bool isAbsolutePath = false)
        {
            if (file == null)
            {
                throw new FileNotAttachedException();
            }
            if (!isAbsolutePath)
            {
                pathOfFolder = _fileBasePath + pathOfFolder;
            }
            if (!Directory.Exists(pathOfFolder))
            {
                Directory.CreateDirectory(pathOfFolder);
            }
            var path = Path.Combine(pathOfFolder, file.FileName);
            file.SaveAs(path);
            return path;
        }

        public string UploadFile(HttpPostedFileBase file, string pathOfFolder, string fullName,
            bool isAbsolutePath = false)
        {
            if (file == null)
            {
                throw new FileNotAttachedException();
            }
            if (!isAbsolutePath)
            {
                pathOfFolder = _fileBasePath + pathOfFolder;
            }
            if (!Directory.Exists(pathOfFolder))
            {
                Directory.CreateDirectory(pathOfFolder);
            }
            var path = Path.Combine(pathOfFolder, fullName);
            file.SaveAs(path);
            return path;
        }

        public void DeleteFile(string path, bool isAbsolutePath = false)
        {
            var fullPath = _fileBasePath + path;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public IFileResultModel GetFile(string pathOfFolder, string fileName, bool isAbsolutePath = false)
        {
            if (!isAbsolutePath)
            {
                pathOfFolder = _fileBasePath + pathOfFolder;
            }
            return new FileResultModel
            {
                ContentType = MimeMapping.GetMimeMapping(fileName),
                FullPath = pathOfFolder + fileName
            };
        }

        public string DownloadAndSaveUserFile(string directoryName, string serverUrl, string fileName)
        {
            if (!Directory.Exists(_userFilesPath + $"/Users/{directoryName}"))
                Directory.CreateDirectory(_userFilesPath + $"/Users/{directoryName}");
            var myWebClient = new WebClient();
            myWebClient.DownloadFile(serverUrl, _userFilesPath + $"/Users/{directoryName}/{fileName}");
            return _userFilesPath + $"/Users/{directoryName}/{fileName}";
        }

        public string UploadFile(byte[] file, string pathOfFolder, string fullName, bool isAbsolutePath = false)
        {
            if (file == null)
            {
                throw new FileNotAttachedException();
            }
            if (!isAbsolutePath)
            {
                pathOfFolder = _fileBasePath + pathOfFolder;
            }
            if (!Directory.Exists(pathOfFolder))
            {
                Directory.CreateDirectory(pathOfFolder);
            }
            var path = Path.Combine(pathOfFolder, fullName);
            File.WriteAllBytes(path, file);
            return path;
        }

         public bool FileExists(string path, bool isAbsolutePath = false)
        {
            if (!isAbsolutePath)
            {
                path = _fileBasePath + path;
            }
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }

        public string GetFullFilePath(string relativeFilePath, bool isAbsolutePath = false)
        {
            if (!isAbsolutePath)
            {
                relativeFilePath = _fileBasePath + relativeFilePath;
            }
            return relativeFilePath;
        }
    }
}
