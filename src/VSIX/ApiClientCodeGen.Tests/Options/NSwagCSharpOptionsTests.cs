using Rapicgen.Core.Options.NSwag;
using Rapicgen.Options.NSwag;
using FluentAssertions;
using Moq;

namespace Rapicgen.Tests.Options
{
    
    public class NSwagCSharpOptionsTests
    {
        private readonly INSwagOptions options;

        public NSwagCSharpOptionsTests()
            => options = new Mock<INSwagOptions>().Object;

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagCSharpOptions(options)
                .InjectHttpClient
                .Should()
                .Be(options.InjectHttpClient);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateClientInterfaces
                .Should()
                .Be(options.GenerateClientInterfaces);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateDtoTypes
                .Should()
                .Be(options.GenerateDtoTypes);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagCSharpOptions(options)
                .UseBaseUrl
                .Should()
                .Be(options.UseBaseUrl);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => new NSwagCSharpOptions(options)
                .ClassStyle
                .Should()
                .Be(options.ClassStyle);

        [Xunit.Fact]
        public void Reads_UseDocumentTitle_From_Options()
            => new NSwagCSharpOptions(options)
                .UseDocumentTitle
                .Should()
                .Be(options.UseDocumentTitle);
    }
}
