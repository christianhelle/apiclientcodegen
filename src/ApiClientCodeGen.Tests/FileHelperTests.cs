using System.IO;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class FileHelperTests
    {
        [TestMethod]
        public void ReadThenDelete_Reads_File_Contents()
        {
            var tempFile = Path.GetTempFileName();
            var contents = new Fixture().Create<string>();
            File.WriteAllText(tempFile, contents);
            FileHelper.ReadThenDelete(tempFile)
                .Should()
                .Be(contents);
        }

        [TestMethod]
        public void ReadThenDelete_Removes_File()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, new Fixture().Create<string>());
            FileHelper.ReadThenDelete(tempFile);
            File.Exists(tempFile).Should().BeFalse();
        }

        [TestMethod]
        public void CalculateChecksum_Always_Returns_Same_Hash()
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, new Fixture().Create<string>());
            
            FileHelper.CalculateChecksum(tempFile)
                .Should()
                .NotBeNullOrWhiteSpace()
                .And
                .Be(FileHelper.CalculateChecksum(tempFile));
        }
    }
}
