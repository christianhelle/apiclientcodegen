using System;
using System.IO;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class LongPathCSharpFileMergerTests
    {
        [Fact]
        public void Can_Handle_Extremely_Long_Paths_Over_260_Characters()
        {
            // Arrange: Create a path longer than 260 characters to simulate Windows issue
            var tempPath = Path.GetTempPath();
            var baseGuid = Guid.NewGuid().ToString();
            
            // Build a path that's definitely longer than 260 characters
            var longPathComponents = new[]
            {
                baseGuid,
                "ExtremelyLongDirectoryNameThatExceedsWindowsLimitations",
                "AnotherVeryLongDirectoryNameWithManyCharacters",
                "YetAnotherLongDirectoryWithEvenMoreCharacters",
                "FinalDirectoryWithAnExtremelyLongNameToEnsureWeExceed260Characters",
                "SubDirectoryThatMakesThePathEvenLongerThanBefore"
            };
            
            var longPath = Path.Combine(tempPath, Path.Combine(longPathComponents));
            
            // Ensure the path is longer than 260 characters
            var testFileName = "VeryLongFileNameThatAddsEvenMoreCharactersToTheAlreadyLongPath.cs";
            var fullPath = Path.Combine(longPath, testFileName);
            
            // Skip test if the path would be too long for the filesystem
            if (fullPath.Length <= 260)
            {
                // Add more components to make it longer
                longPath = Path.Combine(longPath, "ExtraLongSubDirectory", "AnotherExtraLongSubDirectory");
                fullPath = Path.Combine(longPath, testFileName);
            }
            
            try
            {
                // Create the directory structure
                Directory.CreateDirectory(longPath);
                
                // Create test C# files with long paths
                var csharpContent1 = @"using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratedNamespaceWithVeryLongName
{
    public class VeryLongClassNameThatShouldBeHandledProperly
    {
        public void TestMethodWithLongName() 
        { 
            // This is a test method
        }
    }
}";

                var csharpContent2 = @"using System.Threading.Tasks;
using System.Text.Json;

namespace GeneratedNamespaceWithVeryLongName.SubNamespace
{
    public class AnotherVeryLongClassNameForTesting
    {
        public async Task<string> AnotherTestMethodAsync() 
        { 
            return await Task.FromResult(""test"");
        }
    }
}";

                File.WriteAllText(fullPath, csharpContent1);
                
                var secondFile = Path.Combine(longPath, "AnotherVeryLongFileName.cs");
                File.WriteAllText(secondFile, csharpContent2);
                
                // Act: Try to merge files with long paths
                var result = CSharpFileMerger.MergeFiles(Path.Combine(tempPath, baseGuid));
                
                // Assert: The merge should succeed and contain expected content
                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Contain("using System;");
                result.Should().Contain("using System.Collections.Generic;");
                result.Should().Contain("using System.Net.Http;");
                result.Should().Contain("using System.Threading.Tasks;");
                result.Should().Contain("using System.Text.Json;");
                result.Should().Contain("using Microsoft.Extensions.DependencyInjection;");
                result.Should().Contain("VeryLongClassNameThatShouldBeHandledProperly");
                result.Should().Contain("AnotherVeryLongClassNameForTesting");
                
                // Verify the path was actually long
                fullPath.Length.Should().BeGreaterThan(200, "Path should be significantly long to test long path handling");
            }
            finally
            {
                // Cleanup: Remove the temporary directory
                try
                {
                    if (Directory.Exists(Path.Combine(tempPath, baseGuid)))
                    {
                        Directory.Delete(Path.Combine(tempPath, baseGuid), true);
                    }
                }
                catch (Exception)
                {
                    // Ignore cleanup errors as they're not part of the test
                }
            }
        }
    }
}