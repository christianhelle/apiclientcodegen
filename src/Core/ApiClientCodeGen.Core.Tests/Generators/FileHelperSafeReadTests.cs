using System;
using System.IO;
using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators
{
    public class FileHelperSafeReadTests
    {
        [Fact]
        public void SafeReadAllLines_Should_Read_File_Content()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            var expected = new[] { "Line 1", "Line 2", "Line 3" };
            File.WriteAllLines(tempFile, expected);

            try
            {
                // Act
                var lines = FileHelper.SafeReadAllLines(tempFile);

                // Assert
                lines.Should().BeEquivalentTo(expected);
            }
            finally
            {
                // Cleanup
                File.Delete(tempFile);
            }
        }

        [Fact(Skip = "Only meant to be run manually to test very long paths")]
        public void SafeReadAllLines_Should_Handle_Long_Paths()
        {
            // This test is skipped and only meant to be run manually
            // on Windows systems with long paths to verify the fix
        }
    }
}