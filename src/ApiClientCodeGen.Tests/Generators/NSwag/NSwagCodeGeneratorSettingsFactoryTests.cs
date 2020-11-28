using System.Threading.Tasks;
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
        public async Task Does_Not_Return_Null()
            => new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    Test.CreateDummy<INSwagOptions>())
                .GetGeneratorSettings(
                    await new OpenApiDocumentFactory()
                        .GetDocument(SwaggerJsonFilename))
                .Should()
                .NotBeNull();
    }
}