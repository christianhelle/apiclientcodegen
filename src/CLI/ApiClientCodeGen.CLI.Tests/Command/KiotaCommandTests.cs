using System.Linq;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using FluentAssertions;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Generators.Kiota;
using Xunit;

namespace Rapicgen.CLI.Tests.Command;

public class KiotaCommandTests
{
    [Theory, AutoMoqData]
    public void CreateGenerator_WhenCalled_ReturnsKiotaCodeGenerator(
        KiotaCommand sut)
    {
        var result = sut.CreateGenerator();
        Assert.IsType<KiotaCodeGenerator>(result);
    }
    
    [Fact]
    public void HasAttribute_Command()
    {
        typeof(KiotaCommand)
            .GetCustomAttributes(false)
            .Should()
            .Contain(x => x is CommandAttribute);
    }
    
    [Fact]
    public void CommandAttribute_Name_Is_Kiota()
    {
        typeof(KiotaCommand)
            .GetCustomAttributes(false)
            .OfType<CommandAttribute>()
            .First()
            .Name
            .Should()
            .Be("kiota");
    }
}