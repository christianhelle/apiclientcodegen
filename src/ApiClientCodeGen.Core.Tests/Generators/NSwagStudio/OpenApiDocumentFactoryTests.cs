using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.NSwagStudio
{
    public class OpenApiDocumentFactoryTests : TestWithResources
    {
        [Fact]
        public void Does_Not_Return_Null()
            => new OpenApiDocumentFactory()
                .GetDocument(SwaggerJsonFilename)
                .Should()
                .NotBeNull();
    }
}