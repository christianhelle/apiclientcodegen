using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.NSwagStudio
{
    public class OpenApiDocumentFactoryTests : TestWithResources
    {
        [Fact]
        public async Task Does_Not_Return_Null()
            => (await new OpenApiDocumentFactory().GetDocumentAsync(SwaggerJsonFilename))
                .Should()
                .NotBeNull();
    }
}