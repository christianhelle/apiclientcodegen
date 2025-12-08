using Rapicgen.Core.Options.NSwag;
using Rapicgen.Core.Options.NSwagStudio;
using FluentAssertions;

namespace Rapicgen.Tests.Options
{
    
    public class DefaultNSwagStudioOptionsTests
    {
        private readonly INSwagStudioOptions sut = new DefaultNSwagStudioOptions();
        
        [Xunit.Fact]
        public void ClassStyle_Be_Poco()
            => sut.ClassStyle.Should().Be(CSharpClassStyle.Poco);

        [Xunit.Fact]
        public void GenerateClientInterfaces_Be_True()
            => sut.GenerateClientInterfaces.Should().BeTrue();

        [Xunit.Fact]
        public void GenerateDtoTypes_Be_True()
            => sut.GenerateDtoTypes.Should().BeTrue();

        [Xunit.Fact]
        public void InjectHttpClient_Be_True()
            => sut.InjectHttpClient.Should().BeTrue();

        [Xunit.Fact]
        public void UseBaseUrl_Be_False()
            => sut.UseBaseUrl.Should().BeFalse();

        [Xunit.Fact]
        public void UseDocumentTitle_Be_True()
            => sut.UseDocumentTitle.Should().BeTrue();

        [Xunit.Fact]
        public void GenerateDataAnnotations_BeFalse()
            => sut.GenerateDataAnnotations.Should().BeFalse();

        [Xunit.Fact]
        public void GenerateDefaultValues_BeFalse()
            => sut.GenerateDefaultValues.Should().BeFalse();

        [Xunit.Fact]
        public void GenerateJsonMethods_BeFalse()
            => sut.GenerateJsonMethods.Should().BeFalse();

        [Xunit.Fact]
        public void GenerateResponseClasses_BeFalse()
            => sut.GenerateResponseClasses.Should().BeFalse();

        [Xunit.Fact]
        public void RequiredPropertiesMustBeDefined_BeFalse()
            => sut.RequiredPropertiesMustBeDefined.Should().BeFalse();
    }
}