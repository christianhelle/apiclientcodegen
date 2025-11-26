using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.Core.Options.NSwag;
using FluentAssertions;
using Rapicgen.CLI.Commands.CSharp;
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
            INSwagOptions options)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options)
                .Should()
                .NotBeNull();
    }
}