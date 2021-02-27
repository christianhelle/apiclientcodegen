using System.Xml.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests
{
    public class ProjectFileUpdaterTests
    {
        private const string CSharpProjectFileContents = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>
</Project>";

        private readonly string xml;

        public ProjectFileUpdaterTests()
        {
            var sut = new ProjectFileUpdater(XDocument.Parse(CSharpProjectFileContents));
            var document = sut.UpdatePropertyGroup(AutoRestConstants.PropertyGroups);
            xml = document.ToString();
        }

        [Fact]
        public void Updates_PropertyGroups()
            => xml.Should().NotBeEquivalentTo(CSharpProjectFileContents);

        [Fact]
        public void Sets_IncludeGeneratorSharedCode() 
            => xml.Should().Contain("<IncludeGeneratorSharedCode>true</IncludeGeneratorSharedCode>");

        [Fact]
        public void Sets_RestoreAdditionalProjectSources()
        {
            xml.Should().Contain("<RestoreAdditionalProjectSources>");
            xml.Should().Contain("</RestoreAdditionalProjectSources>");
            xml.Should().Contain("https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json");
        }
    }
}