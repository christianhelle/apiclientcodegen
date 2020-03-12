using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class DefaultNSwagStudioOptionsTests
    {
        private readonly INSwagStudioOptions sut = new DefaultNSwagStudioOptions();
        
        [TestMethod]
        public void ClassStyle_Be_Poco()
            => sut.ClassStyle.Should().Be(CSharpClassStyle.Poco);

        [TestMethod]
        public void GenerateClientInterfaces_Be_True()
            => sut.GenerateClientInterfaces.Should().BeTrue();

        [TestMethod]
        public void GenerateDtoTypes_Be_True()
            => sut.GenerateDtoTypes.Should().BeTrue();

        [TestMethod]
        public void InjectHttpClient_Be_True()
            => sut.InjectHttpClient.Should().BeTrue();

        [TestMethod]
        public void UseBaseUrl_Be_False()
            => sut.UseBaseUrl.Should().BeFalse();

        [TestMethod]
        public void UseDocumentTitle_Be_True()
            => sut.UseDocumentTitle.Should().BeTrue();

        [TestMethod]
        public void GenerateDataAnnotations_BeFalse()
            => sut.GenerateDataAnnotations.Should().BeFalse();

        [TestMethod]
        public void GenerateDefaultValues_BeFalse()
            => sut.GenerateDefaultValues.Should().BeFalse();

        [TestMethod]
        public void GenerateJsonMethods_BeFalse()
            => sut.GenerateJsonMethods.Should().BeFalse();

        [TestMethod]
        public void GenerateResponseClasses_BeFalse()
            => sut.GenerateResponseClasses.Should().BeFalse();

        [TestMethod]
        public void RequiredPropertiesMustBeDefined_BeFalse()
            => sut.RequiredPropertiesMustBeDefined.Should().BeFalse();
    }
}