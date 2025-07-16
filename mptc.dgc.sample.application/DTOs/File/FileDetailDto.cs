namespace mptc.dgc.sample.application.DTOs.File
{
    public class FileDetailDto
    {
        public string FileName { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
