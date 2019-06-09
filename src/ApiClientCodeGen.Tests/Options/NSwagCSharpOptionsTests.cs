using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class NSwagCSharpOptionsTests
    {
        private INSwagOption options;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            var mock = new Mock<INSwagOption>();
            options = mock.Object;
        }

        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagCSharpOptions(options)
                .InjectHttpClient
                .Should()
                .Be(options.InjectHttpClient);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateClientInterfaces
                .Should()
                .Be(options.GenerateClientInterfaces);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagCSharpOptions(options)
                .GenerateDtoTypes
                .Should()
                .Be(options.GenerateDtoTypes);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagCSharpOptions(options)
                .UseBaseUrl
                .Should()
                .Be(options.UseBaseUrl);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => new NSwagCSharpOptions(options)
                .ClassStyle
                .Should()
                .Be(options.ClassStyle);
    }
}
