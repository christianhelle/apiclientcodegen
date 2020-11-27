using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag;
using FluentAssertions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwag
{ 
    public class NSwagCodeGeneratorSettingsFactoryTests : TestWithResources
    {
        [Xunit.Fact]
        public void Does_Not_Return_Null()
            => new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    Test.CreateDummy<INSwagOptions>())
                .GetGeneratorSettings(
                    new OpenApiDocumentFactory()
                        .GetDocument(SwaggerJsonFilename))
                .Should()
                .NotBeNull();
    }
}