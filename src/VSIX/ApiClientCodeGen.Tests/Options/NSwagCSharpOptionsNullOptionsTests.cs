using Rapicgen.Core.Options.NSwag;
using Rapicgen.Options.NSwag;
using FluentAssertions;

namespace Rapicgen.Tests.Options
{
    
    public class NSwagCSharpOptionsNullOptionsTests
    {
        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagCSharpOptions(null)
                .InjectHttpClient
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagCSharpOptions()
                .GenerateClientInterfaces
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagCSharpOptions()
                .GenerateDtoTypes
                .Should()
                .Be(true);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagCSharpOptions()
                .UseBaseUrl
                .Should()
                .Be(false);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => new NSwagCSharpOptions()
                .ClassStyle
                .Should()
                .Be(CSharpClassStyle.Poco);
    }
}