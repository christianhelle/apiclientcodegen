using Rapicgen.Core.Options.NSwag;
using FluentAssertions;

namespace Rapicgen.Tests.Options
{
    
    public class DefaultNSwagOptionsTests 
    {
        private readonly INSwagOptions sut = new DefaultNSwagOptions();

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
    }
}