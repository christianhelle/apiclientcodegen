using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;
using FluentAssertions;
using Xunit;

namespace Rapicgen.CLI.Tests.Command
{
    public class NSwagCodeGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            NSwagCodeGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            INSwagOptions options,
            IOpenApiDocumentFactory documentFactory)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    documentFactory)
                .Should()
                .NotBeNull();
    }
}