using Moq;
using mptc.dgc.sample.application.DTOs.File;
using mptc.dgc.sample.application.Services;
using Xunit;

namespace mptc.dgc.sample.test
{
    public class SftpUnitTest
    {

        [Fact]
        public void TestUploadFile()
        {

            var mockClient = new Mock<ISftpService>();
            var mockedInstance = mockClient.Object;

            var localPath = "test.txt";
            var remotePath = "remote.txt";

            // Act
            mockedInstance.UploadFile(localPath, remotePath);

            // Assert
            mockClient.Verify(s => s.UploadFile(localPath, remotePath), Times.Once);

        }
        [Fact]
        public void TestReadDetailFile()
        {
            var mockSftpClient = new Mock<ISftpService>();
            var mockedInstance = mockSftpClient.Object;
            var fakeFiles = new List<FileDetailDto>
            {
                new FileDetailDto { FileName = "file1.txt", Size = 100 },
                new FileDetailDto { FileName = "file2.txt", Size = 200 }
            };
            mockSftpClient.Setup(s => s.ReadAllFile()).Returns(fakeFiles);
            // Act
            var result = mockedInstance.ReadAllFile();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, f => f.FileName == "file1.txt" && f.Size == 100);
            Assert.Contains(result, f => f.FileName == "file2.txt" && f.Size == 200);
        }
    }
}
