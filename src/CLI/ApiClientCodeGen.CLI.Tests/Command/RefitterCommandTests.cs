using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Moq;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Options.Refitter;
using Xunit;

namespace Rapicgen.CLI.Tests.Command;

public class RefitterCommandTests
{
    [Theory, AutoMoqData]
    public void Should_Create_From_Factory(
        [Frozen] IRefitterOptions options,
        [Frozen] IRefitterCodeGeneratorFactory factory,
        RefitterCommand sut)
    {
        sut.OnExecute();
        Mock.Get(factory)
            .Verify(
                f => f.Create(
                    sut.SwaggerFile,
                    sut.DefaultNamespace,
                    options));
    }
}