using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class NSwagCSharpOptionsTests
    {
        private INSwagOptions options;

        [TestInitialize]
        public void Init()
            => options = new Mock<INSwagOptions>().Object;

        [TestMethod, Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagCSharpOptions(options)
                .InjectHttpClient
                .Should()
                .Be(options.InjectHttpClient);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateClientInterfaces
                .Should()
                .Be(options.GenerateClientInterfaces);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateDtoTypes
                .Should()
                .Be(options.GenerateDtoTypes);

        [TestMethod, Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagCSharpOptions(options)
                .UseBaseUrl
                .Should()
                .Be(options.UseBaseUrl);

        [TestMethod, Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => new NSwagCSharpOptions(options)
                .ClassStyle
                .Should()
                .Be(options.ClassStyle);
    }
}
