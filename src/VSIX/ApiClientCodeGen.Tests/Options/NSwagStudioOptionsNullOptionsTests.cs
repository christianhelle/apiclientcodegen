using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;
using Rapicgen.Options.NSwagStudio;
using FluentAssertions;

namespace Rapicgen.Tests.Options
{
    
    public class NSwagStudioOptionsNullOptionsTests
    {
        [Xunit.Fact]
        public void Implements_INSwagStudioOptions() 
            => typeof(NSwagStudioOptions)
                .Should()
                .BeAssignableTo<INSwagStudioOptions>();

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagStudioOptions()
                .InjectHttpClient
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagStudioOptions()
                .GenerateClientInterfaces
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagStudioOptions()
                .GenerateDtoTypes
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagStudioOptions()
                .UseBaseUrl
                .Should()
                .Be(false);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => new NSwagStudioOptions()
                .ClassStyle
                .Should()
                .Be(CSharpClassStyle.Poco);

        [Xunit.Fact]
        public void Reads_GenerateResponseClasses_From_Options()
            => new NSwagStudioOptions()
                .GenerateResponseClasses
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateJsonMethods_From_Options()
            => new NSwagStudioOptions()
                .GenerateJsonMethods
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => new NSwagStudioOptions()
                .RequiredPropertiesMustBeDefined
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateDefaultValues_From_Options()
            => new NSwagStudioOptions()
                .GenerateDefaultValues
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateDataAnnotations_From_Options()
            => new NSwagStudioOptions()
                .GenerateDataAnnotations
                .Should()
                .Be(true);
    }
}