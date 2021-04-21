using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    
    public class NSwagStudioOptionsTests
    {
        private readonly INSwagStudioOptions options;

        public NSwagStudioOptionsTests()
        {
            var mock = new Mock<INSwagStudioOptions>();
            options = mock.Object;
        }

        [Xunit.Fact]
        public void Implements_INSwagStudioOptions() 
            => typeof(NSwagStudioOptions)
                .Should()
                .BeAssignableTo<INSwagStudioOptions>();

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagStudioOptions(options)
                .InjectHttpClient
                .Should()
                .Be(options.InjectHttpClient);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateClientInterfaces
                .Should()
                .Be(options.GenerateClientInterfaces);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDtoTypes
                .Should()
                .Be(options.GenerateDtoTypes);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagStudioOptions(options)
                .UseBaseUrl
                .Should()
                .Be(options.UseBaseUrl);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => new NSwagStudioOptions(options)
                .ClassStyle
                .Should()
                .Be(options.ClassStyle);

        [Xunit.Fact]
        public void Reads_GenerateResponseClasses_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateResponseClasses
                .Should()
                .Be(options.GenerateResponseClasses);

        [Xunit.Fact]
        public void Reads_GenerateJsonMethods_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateJsonMethods
                .Should()
                .Be(options.GenerateJsonMethods);

        [Xunit.Fact]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => new NSwagStudioOptions(options)
                .RequiredPropertiesMustBeDefined
                .Should()
                .Be(options.RequiredPropertiesMustBeDefined);

        [Xunit.Fact]
        public void Reads_GenerateDefaultValues_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDefaultValues
                .Should()
                .Be(options.GenerateDefaultValues);

        [Xunit.Fact]
        public void Reads_GenerateDataAnnotations_From_Options()
            => new NSwagStudioOptions(options)
                .GenerateDataAnnotations
                .Should()
                .Be(options.GenerateDataAnnotations);

        [Xunit.Fact]
        public void Reads_UseDocumentTitle_From_Options()
            => new NSwagStudioOptions(options)
                .UseDocumentTitle
                .Should()
                .Be(options.UseDocumentTitle);
    }
}
