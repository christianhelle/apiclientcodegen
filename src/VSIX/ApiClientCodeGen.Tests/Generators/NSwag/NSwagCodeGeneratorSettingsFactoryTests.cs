using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;
using FluentAssertions;

namespace Rapicgen.Tests.Generators.NSwag
{ 
    public class NSwagCodeGeneratorSettingsFactoryTests : TestWithResources
    {
        [Xunit.Fact]
        public async Task Does_Not_Return_Null_Async()
            => new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    Test.CreateDummy<INSwagOptions>())
                .GetGeneratorSettings(
                    await new OpenApiDocumentFactory()
                        .GetDocumentAsync(SwaggerJsonFilename))
                .Should()
                .NotBeNull();
    }
}