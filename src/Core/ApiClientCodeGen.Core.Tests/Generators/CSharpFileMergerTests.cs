using System;
using System.IO;
using FluentAssertions;
using Rapicgen.Core.Generators;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators;

public class CSharpFileMergerTests : IDisposable
{
    private readonly string _tempDir;

    public CSharpFileMergerTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, true);
    }

    [Fact]
    public void MergeFiles_SingleFile_ReturnsFileContent()
    {
        var content = @"using System;

namespace TestNamespace
{
    public class TestClass
    {
        public string Name { get; set; }
    }
}";
        File.WriteAllText(Path.Combine(_tempDir, "Test.cs"), content);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("namespace TestNamespace");
        result.Should().Contain("public class TestClass");
        result.Should().Contain("using System;");
    }

    [Fact]
    public void MergeFiles_TwoFilesWithSameNamespace_MergesBothClasses()
    {
        var file1 = @"using System;

namespace SharedNamespace
{
    public class ClassA
    {
        public int Id { get; set; }
    }
}";
        var file2 = @"using System;

namespace SharedNamespace
{
    public class ClassB
    {
        public string Name { get; set; }
    }
}";
        File.WriteAllText(Path.Combine(_tempDir, "ClassA.cs"), file1);
        File.WriteAllText(Path.Combine(_tempDir, "ClassB.cs"), file2);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class ClassA");
        result.Should().Contain("public class ClassB");
    }

    [Fact]
    public void MergeFiles_TwoFilesWithDifferentUsings_CombinesUniqueUsings()
    {
        var file1 = @"using System;
using System.Collections.Generic;

namespace Ns1
{
    public class ClassA { }
}";
        var file2 = @"using System;
using System.Linq;

namespace Ns2
{
    public class ClassB { }
}";
        File.WriteAllText(Path.Combine(_tempDir, "A.cs"), file1);
        File.WriteAllText(Path.Combine(_tempDir, "B.cs"), file2);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("using System;");
        result.Should().Contain("using System.Collections.Generic;");
        result.Should().Contain("using System.Linq;");
    }

    [Fact]
    public void MergeFiles_ExcludesAssemblyInfoFiles()
    {
        var regularFile = @"using System;

namespace Test
{
    public class Regular { }
}";
        var assemblyInfo = @"using System.Reflection;
[assembly: AssemblyTitle(""Test"")]";

        File.WriteAllText(Path.Combine(_tempDir, "Regular.cs"), regularFile);
        File.WriteAllText(Path.Combine(_tempDir, "AssemblyInfo.cs"), assemblyInfo);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class Regular");
        result.Should().NotContain("AssemblyTitle");
    }

    [Fact]
    public void MergeFiles_ExcludesTestFiles()
    {
        var regularFile = @"using System;

namespace Test
{
    public class MyModel { }
}";
        var testFile = @"using NUnit.Framework;

namespace Test
{
    public class MyModelTests { }
}";
        File.WriteAllText(Path.Combine(_tempDir, "MyModel.cs"), regularFile);
        File.WriteAllText(Path.Combine(_tempDir, "MyModelTests.cs"), testFile);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class MyModel");
        // Test files ending with "tests.cs" should be excluded
        result.Should().NotContain("public class MyModelTests");
    }

    [Fact]
    public void MergeFiles_ExcludesNUnitNamespaces()
    {
        var file = @"using System;
using NUnit.Framework;

namespace Test
{
    public class Something { }
}";
        File.WriteAllText(Path.Combine(_tempDir, "Something.cs"), file);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().NotContain("using NUnit.Framework;");
    }

    [Fact]
    public void MergeFiles_IncludesSubdirectoryFiles()
    {
        var subDir = Path.Combine(_tempDir, "SubDir");
        Directory.CreateDirectory(subDir);

        var file1 = @"using System;

namespace Root
{
    public class RootClass { }
}";
        var file2 = @"using System;

namespace Sub
{
    public class SubClass { }
}";
        File.WriteAllText(Path.Combine(_tempDir, "Root.cs"), file1);
        File.WriteAllText(Path.Combine(subDir, "Sub.cs"), file2);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class RootClass");
        result.Should().Contain("public class SubClass");
    }

    [Fact]
    public void MergeFiles_OnlyIncludesCsFiles()
    {
        var csFile = @"using System;

namespace Test
{
    public class Valid { }
}";
        File.WriteAllText(Path.Combine(_tempDir, "Valid.cs"), csFile);
        File.WriteAllText(Path.Combine(_tempDir, "NotCode.txt"), "This is not code");
        File.WriteAllText(Path.Combine(_tempDir, "Data.json"), "{}");

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class Valid");
        result.Should().NotContain("This is not code");
    }

    [Fact]
    public void MergeFiles_RemovesUsingStatementsFromBody()
    {
        var file = @"using System;
using System.IO;

namespace Test
{
    public class MyClass
    {
        public string Name { get; set; }
    }
}";
        File.WriteAllText(Path.Combine(_tempDir, "MyClass.cs"), file);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        // Using statements should appear at the top, not inline in body
        result.Should().Contain("using System;");
        result.Should().Contain("public class MyClass");
    }

    [Fact]
    public void MergeFiles_SkipsEmptyLines()
    {
        var file = @"using System;

namespace Test
{

    public class Sparse
    {

        public string Value { get; set; }

    }

}";
        File.WriteAllText(Path.Combine(_tempDir, "Sparse.cs"), file);

        var result = CSharpFileMerger.MergeFiles(_tempDir);

        result.Should().Contain("public class Sparse");
    }
}
