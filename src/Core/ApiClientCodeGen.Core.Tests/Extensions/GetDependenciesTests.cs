using System.Linq;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.Extensions
{
    public class GetDependenciesTests
    {
        [Xunit.Fact]
        public void GetDependencies_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Any(c => c.Name == "Microsoft.Rest.ClientRuntime")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_NSwag()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Any(c => c.Name == "Newtonsoft.Json")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_Swagger()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Any(c => c.Name == "RestSharp" || c.Name == "JsonSubTypes")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_OpenApi()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Any(c => c.Name == "Microsoft.Extensions.Http" || c.Name == "Microsoft.Extensions.Hosting" || c.Name == "Microsoft.Extensions.Http.Polly" || c.Name == "System.Threading.Channels" || c.Name == "System.ComponentModel.Annotations")
                .Should()
                .BeTrue();
    }
}
