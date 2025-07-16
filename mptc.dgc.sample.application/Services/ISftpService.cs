using mptc.dgc.sample.application.DTOs.File;

namespace mptc.dgc.sample.application.Services
{
    public interface ISftpService
    {
        void UploadFile(string localFilePath, string remoteFileName);
        List<FileDetailDto> ReadAllFile();
        FileDetailDto ReadDetailFile();
    }
}
