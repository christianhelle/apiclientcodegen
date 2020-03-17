using System.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class GetDependenciesTests
    {
        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Any(c => c.Name == "Microsoft.Rest.ClientRuntime")
                .Should()
                .BeTrue();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Any(c => c.Name == "Newtonsoft.Json")
                .Should()
                .BeTrue();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Any(c => c.Name == "RestSharp" || c.Name == "JsonSubTypes")
                .Should()
                .BeTrue();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Any(c => c.Name == "RestSharp" || c.Name == "JsonSubTypes")
                .Should()
                .BeTrue();
    }
}
