using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class NSwagStudioOptionsTests
    {
        private INSwagStudioOptions options;

        [TestInitialize]
        public void Init()
        {
            var mock = new Mock<INSwagStudioOptions>();
            options = mock.Object;
        }

        [TestMethod]
        public void Extends_NSwagCSharpOptions() 
            => typeof(NSwagStudioOptions)
                .Should()
                .BeAssignableTo<NSwagCSharpOptions>();

        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagStudioOptions(options)
                .InjectHttpClient
                .Should()
                .Be(options.InjectHttpClient);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateClientInterfaces
                .Should()
                .Be(options.GenerateClientInterfaces);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDtoTypes
                .Should()
                .Be(options.GenerateDtoTypes);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagStudioOptions(options)
                .UseBaseUrl
                .Should()
                .Be(options.UseBaseUrl);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => new NSwagStudioOptions(options)
                .ClassStyle
                .Should()
                .Be(options.ClassStyle);

        [TestMethod]
        public void Reads_GenerateResponseClasses_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateResponseClasses
                .Should()
                .Be(options.GenerateResponseClasses);

        [TestMethod]
        public void Reads_GenerateJsonMethods_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateJsonMethods
                .Should()
                .Be(options.GenerateJsonMethods);

        [TestMethod]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => new NSwagStudioOptions(options)
                .RequiredPropertiesMustBeDefined
                .Should()
                .Be(options.RequiredPropertiesMustBeDefined);

        [TestMethod]
        public void Reads_GenerateDefaultValues_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDefaultValues
                .Should()
                .Be(options.GenerateDefaultValues);

        [TestMethod]
        public void Reads_GenerateDataAnnotations_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDataAnnotations
                .Should()
                .Be(options.GenerateDataAnnotations);
    }
}
