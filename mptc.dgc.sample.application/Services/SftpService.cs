using mptc.dgc.sample.application.DTOs.File;
using Renci.SshNet;

namespace mptc.dgc.sample.application.Services
{
    public class SftpService : ISftpService
    {
        private readonly SftpConfig _config;

        public SftpService(SftpConfig config)
        {
            _config = config;
        }

        public void UploadFile(string localFilePath, string remotePath)
        {
            using var sftp = new SftpClient(_config.Host, _config.Port, _config.Username, _config.Password);
            sftp.Connect();

            if (FileExists(remotePath))
            {
                Console.WriteLine("Remote file already exists. Skipping upload.");
                return;
            }


            using var fileStream = File.OpenRead(localFilePath);
            sftp.UploadFile(fileStream, remotePath, true);

            sftp.Disconnect();
        }

        public List<FileDetailDto> ReadAllFile()
        {
            using var sftp = new SftpClient(_config.Host, _config.Port, _config.Username, _config.Password);
            sftp.Connect();

            var files = sftp.ListDirectory(_config.RemoteDirectory)
                .Where(f => !f.IsDirectory && !f.IsSymbolicLink)
                .Select(f => new FileDetailDto
                {
                    FileName = f.Name,
                    Size = f.Length,
                    LastModified = f.LastWriteTime
                })
                .ToList();

            sftp.Disconnect();
            return files;
        }

        public FileDetailDto ReadDetailFile()
        {
            throw new NotImplementedException();
        }

        private bool FileExists(string remoteFileName)
        {
            using var sftp = new SftpClient(_config.Host, _config.Port, _config.Username, _config.Password);
            sftp.Connect();

            var remotePath = Path.Combine(_config.RemoteDirectory, remoteFileName).Replace("\\", "/");

            bool exists = sftp.Exists(remotePath);

            sftp.Disconnect();

            return exists;
        }
    }
}
