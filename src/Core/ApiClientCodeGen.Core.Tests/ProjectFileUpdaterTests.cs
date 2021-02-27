using System;
using System.IO;
using System.Xml.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests
{
    public class ProjectFileUpdaterTests
    {
        private const string CSharpProjectFileContentsWithout = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>
</Project>";

        private const string CSharpProjectFileContentsWith = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <IncludeGeneratorSharedCode>false</IncludeGeneratorSharedCode>
    <RestoreAdditionalProjectSources>empty</RestoreAdditionalProjectSources>
  </PropertyGroup>
</Project>";

        private static string ArrangeAndAct(string contents = null)
        {
            var sut = new ProjectFileUpdater(XDocument.Parse(contents ?? CSharpProjectFileContentsWithout));
            var document = sut.UpdatePropertyGroup(AutoRestConstants.PropertyGroups);
            return document.ToString();
        }

        [Fact]
        public void Constructor_Requires_XDocument()
            => new Action(() => new ProjectFileUpdater(null as XDocument))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Fact]
        public void UpdatePropertyGroup_Returns_NotNull()
        {
            var projectFile = Path.GetTempFileName();
            File.WriteAllText(projectFile, CSharpProjectFileContentsWithout);
            var sut = new ProjectFileUpdater(projectFile);
            sut.UpdatePropertyGroup(AutoRestConstants.PropertyGroups)
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void Updates_PropertyGroups()
            => ArrangeAndAct().Should().NotBeEquivalentTo(CSharpProjectFileContentsWithout);

        [Fact]
        public void Sets_IncludeGeneratorSharedCode()
            => ArrangeAndAct().Should().Contain("<IncludeGeneratorSharedCode>True</IncludeGeneratorSharedCode>");

        [Fact]
        public void Sets_RestoreAdditionalProjectSources()
        {
            var xml = ArrangeAndAct();
            xml.Should().Contain("<RestoreAdditionalProjectSources>");
            xml.Should().Contain("</RestoreAdditionalProjectSources>");
            xml.Should().Contain("https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json");
        }

        [Fact]
        public void Updates_Existing_PropertyGroups()
            => ArrangeAndAct(CSharpProjectFileContentsWith)
                .Should()
                .NotBeEquivalentTo(CSharpProjectFileContentsWith);

        [Fact]
        public void Sets_Existing_IncludeGeneratorSharedCode()
            => ArrangeAndAct(CSharpProjectFileContentsWith)
                .Should()
                .Contain("<IncludeGeneratorSharedCode>True</IncludeGeneratorSharedCode>");

        [Fact]
        public void Sets_Existing_RestoreAdditionalProjectSources()
        {
            var xml = ArrangeAndAct(CSharpProjectFileContentsWith);
            xml.Should().Contain("<RestoreAdditionalProjectSources>");
            xml.Should().Contain("</RestoreAdditionalProjectSources>");
            xml.Should().Contain("https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json");
        }
    }
}