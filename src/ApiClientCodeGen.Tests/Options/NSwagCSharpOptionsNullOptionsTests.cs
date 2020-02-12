using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class NSwagCSharpOptionsNullOptionsTests
    {
        [TestMethod]
        public void Reads_InjectHttpClient_From_Options()
            => new NSwagCSharpOptions(null)
                .InjectHttpClient
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateClientInterfaces_From_Options()
            => new NSwagCSharpOptions()
                .GenerateClientInterfaces
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_GenerateDtoTypes_From_Options()
            => new NSwagCSharpOptions()
                .GenerateDtoTypes
                .Should()
                .Be(true);

        [TestMethod]
        public void Reads_UseBaseUrl_From_Options()
            => new NSwagCSharpOptions()
                .UseBaseUrl
                .Should()
                .Be(false);

        [TestMethod]
        public void Reads_ClassStyle_From_Options()
            => new NSwagCSharpOptions()
                .ClassStyle
                .Should()
                .Be(CSharpClassStyle.Poco);
    }
}