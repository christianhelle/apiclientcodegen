using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class NSwagStudioOptionsNullOptionsTests
    {
        [TestMethod]
        public void Implements_INSwagStudioOptions() 
            => typeof(NSwagStudioOptions)
                .Should()
                .BeAssignableTo<INSwagStudioOptions>();

        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagStudioOptions()
                .InjectHttpClient
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagStudioOptions()
                .GenerateClientInterfaces
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagStudioOptions()
                .GenerateDtoTypes
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagStudioOptions()
                .UseBaseUrl
                .Should()
                .Be(false);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => new NSwagStudioOptions()
                .ClassStyle
                .Should()
                .Be(CSharpClassStyle.Poco);

        [TestMethod]
        public void Reads_GenerateResponseClasses_From_Options()
            => new NSwagStudioOptions()
                .GenerateResponseClasses
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateJsonMethods_From_Options()
            => new NSwagStudioOptions()
                .GenerateJsonMethods
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => new NSwagStudioOptions()
                .RequiredPropertiesMustBeDefined
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateDefaultValues_From_Options()
            => new NSwagStudioOptions()
                .GenerateDefaultValues
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateDataAnnotations_From_Options()
            => new NSwagStudioOptions()
                .GenerateDataAnnotations
                .Should()
                .Be(true);
    }
}