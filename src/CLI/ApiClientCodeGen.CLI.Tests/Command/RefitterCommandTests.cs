using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Moq;
using Rapicgen.CLI.Commands.CSharp;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Refitter;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Xunit;

namespace Rapicgen.CLI.Tests.Command;

public class RefitterCommandTests
{
    [Theory, AutoMoqData]
    public void Should_Create_From_Factory(
        [Frozen] IRefitterOptions options,
        [Frozen] IRefitterCodeGeneratorFactory factory,
        [Frozen] IProcessLauncher processLauncher,
        [Frozen] IDependencyInstaller dependencyInstaller,
        RefitterCommand sut,
        RefitterCommandSettings settings)
    {
        // Ensure SettingsFile is null for this test
        settings.SettingsFile = null;
        sut.Execute(null, settings);
        Mock.Get(factory)
            .Verify(f => f.Create(
                settings.SwaggerFile,
                settings.DefaultNamespace,
                processLauncher,
                dependencyInstaller,
                options));
    }

    [Theory, AutoMoqData]
    public void Should_Create_From_Factory_With_Settings_File(
        [Frozen] IRefitterOptions options,
        [Frozen] IRefitterCodeGeneratorFactory factory,
        [Frozen] IProcessLauncher processLauncher,
        [Frozen] IDependencyInstaller dependencyInstaller,
        RefitterCommand sut,
        RefitterCommandSettings settings)
    {
        // Create a temporary file to use as settings file
        var tempFile = Path.GetTempFileName();
        try
        {
            settings.SettingsFile = tempFile;
            sut.Execute(null, settings);

            // Verify that SwaggerFile was updated to match SettingsFile
            Assert.Equal(tempFile, settings.SwaggerFile);

            Mock.Get(factory)
                .Verify(f => f.Create(
                    tempFile,
                    settings.DefaultNamespace,
                    processLauncher,
                    dependencyInstaller,
                    options));
        }
        finally
        {
            // Clean up
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}