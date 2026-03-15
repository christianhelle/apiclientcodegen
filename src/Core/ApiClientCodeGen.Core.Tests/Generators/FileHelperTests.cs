using System;
using System.IO;
using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class FileHelperTests : IDisposable
{
    private readonly string _tempFile;

    public FileHelperTests()
    {
        _tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.tmp");
    }

    public void Dispose()
    {
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
    }

    [Fact]
    public void ReadThenDelete_ReturnsContent()
    {
        const string content = "Hello, World!";
        File.WriteAllText(_tempFile, content);

        var result = FileHelper.ReadThenDelete(_tempFile);

        result.Should().Be(content);
    }

    [Fact]
    public void ReadThenDelete_DeletesFile()
    {
        File.WriteAllText(_tempFile, "test");

        FileHelper.ReadThenDelete(_tempFile);

        File.Exists(_tempFile).Should().BeFalse();
    }

    [Fact]
    public void CreateRandomFile_CreatesFile()
    {
        var file = FileHelper.CreateRandomFile();
        try
        {
            File.Exists(file).Should().BeTrue();
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void CreateRandomFile_ReturnsUniquePaths()
    {
        var file1 = FileHelper.CreateRandomFile();
        var file2 = FileHelper.CreateRandomFile();
        try
        {
            file1.Should().NotBe(file2);
        }
        finally
        {
            File.Delete(file1);
            File.Delete(file2);
        }
    }

    [Fact]
    public void CreateRandomFile_PathIsInTempDirectory()
    {
        var file = FileHelper.CreateRandomFile();
        try
        {
            file.Should().StartWith(Path.GetTempPath());
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void CalculateChecksum_ReturnsNonEmptyString()
    {
        File.WriteAllText(_tempFile, "test content");

        var checksum = FileHelper.CalculateChecksum(_tempFile);

        checksum.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void CalculateChecksum_SameContent_ReturnsSameChecksum()
    {
        File.WriteAllText(_tempFile, "identical content");

        var tempFile2 = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.tmp");
        File.WriteAllText(tempFile2, "identical content");

        try
        {
            var checksum1 = FileHelper.CalculateChecksum(_tempFile);
            var checksum2 = FileHelper.CalculateChecksum(tempFile2);

            checksum1.Should().Be(checksum2);
        }
        finally
        {
            File.Delete(tempFile2);
        }
    }

    [Fact]
    public void CalculateChecksum_DifferentContent_ReturnsDifferentChecksum()
    {
        File.WriteAllText(_tempFile, "content A");

        var tempFile2 = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.tmp");
        File.WriteAllText(tempFile2, "content B");

        try
        {
            var checksum1 = FileHelper.CalculateChecksum(_tempFile);
            var checksum2 = FileHelper.CalculateChecksum(tempFile2);

            checksum1.Should().NotBe(checksum2);
        }
        finally
        {
            File.Delete(tempFile2);
        }
    }
}
