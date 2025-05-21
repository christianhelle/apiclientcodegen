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
        // Ensure SettingsFile is null for this test
        sut.SettingsFile = null;
        sut.OnExecute();
        Mock.Get(factory)
            .Verify(
                f => f.Create(
                    sut.SwaggerFile,
                    sut.DefaultNamespace,
                    options));
    }
    
    [Theory, AutoMoqData]
    public void Should_Create_From_Factory_With_Settings_File(
        [Frozen] IRefitterOptions options,
        [Frozen] IRefitterCodeGeneratorFactory factory,
        string settingsFile,
        RefitterCommand sut)
    {
        sut.SettingsFile = settingsFile;
        sut.OnExecute();
        Mock.Get(factory)
            .Verify(
                f => f.Create(
                    settingsFile,
                    sut.DefaultNamespace,
                    options));
    }
}