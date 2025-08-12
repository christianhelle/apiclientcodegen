using System;
using System.IO;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [Fact]
        public void Can_Merge_CSharp_Files()
        {
            var folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            CSharpFileMerger.MergeFiles(
                    folder)
                .Should()
                .NotBeNullOrWhiteSpace(folder);
        }

        [Fact]
        public void Can_Merge_CSharp_Files_With_Long_Paths()
        {
            // Arrange: Create a temporary directory structure with long paths
            var tempPath = Path.GetTempPath();
            var baseGuid = Guid.NewGuid().ToString();
            var longPath = Path.Combine(tempPath, 
                baseGuid,
                "Item", 
                "ErpBORMAProcSvc",
                "RMADtlsWithCompanyWithRMANumWithRMALine",
                "RMARcptsWithCompanyWithRMANumWithRMALineWithRMAReceipt");
            
            try
            {
                Directory.CreateDirectory(longPath);
                
                // Create a test C# file with a very long path
                var longFileName = Path.Combine(longPath, "RMARcptsWithCompanyWithRMANumWithRMALineWithRMAReceiptRequestBuilder.cs");
                var csharpContent = @"using System;
using System.Net.Http;

namespace GeneratedNamespace
{
    public class RMARcptsWithCompanyWithRMANumWithRMALineWithRMAReceiptRequestBuilder
    {
        public void TestMethod() { }
    }
}";
                File.WriteAllText(longFileName, csharpContent);
                
                // Create another file to test merging
                var anotherFile = Path.Combine(longPath, "AnotherClass.cs");
                var anotherContent = @"using System.Collections.Generic;

namespace GeneratedNamespace
{
    public class AnotherClass
    {
        public void AnotherMethod() { }
    }
}";
                File.WriteAllText(anotherFile, anotherContent);
                
                // Act: Try to merge files (this should not throw an exception)
                var result = CSharpFileMerger.MergeFiles(Path.Combine(tempPath, baseGuid));
                
                // Assert: The merge should succeed and contain the expected content
                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Contain("using System;");
                result.Should().Contain("using System.Collections.Generic;");
                result.Should().Contain("using System.Net.Http;");
                result.Should().Contain("RMARcptsWithCompanyWithRMANumWithRMALineWithRMAReceiptRequestBuilder");
                result.Should().Contain("AnotherClass");
            }
            finally
            {
                // Cleanup: Remove the temporary directory
                if (Directory.Exists(Path.Combine(tempPath, baseGuid)))
                {
                    Directory.Delete(Path.Combine(tempPath, baseGuid), true);
                }
            }
        }
    }
}
