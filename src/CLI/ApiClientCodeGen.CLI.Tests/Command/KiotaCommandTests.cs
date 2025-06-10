using ApiClientCodeGen.Tests.Common.Infrastructure;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Generators.Kiota;
using Xunit;

namespace Rapicgen.CLI.Tests.Command;

public class KiotaCommandTests
{
    [Theory, AutoMoqData]
    public void CreateGenerator_WhenCalled_ReturnsKiotaCodeGenerator(
        KiotaCommand sut,
        KiotaCommandSettings settings)
    {
        var result = sut.CreateGenerator(settings);
        Assert.IsType<KiotaCodeGenerator>(result);
    }
}