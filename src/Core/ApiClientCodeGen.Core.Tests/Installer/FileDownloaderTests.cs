using System;
using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Installer;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Installer
{
    public class FileDownloaderTests
    {
        [Fact]
        public void Implements_Interface()
            => typeof(FileDownloader)
                .Should()
                .Implement<IFileDownloader>();

        [Fact]
        public void Requires_IWebDownloader()
            => new Action(() => new FileDownloader(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public async Task DownloadFile_Invokes_WebDownloader(
            [Frozen] IWebDownloader downloader,
            FileDownloader sut,
            string outputFolder,
            string outputFilename,
            string checksumMd5,
            string url)
        {
            await sut.DownloadFile(
                outputFilename,
                outputFilename,
                checksumMd5,
                url,
                true);

            Mock.Get(downloader)
                .Verify(
                    c => c.DownloadFile(
                        url,
                        It.IsAny<string>()));
        }
    }
}