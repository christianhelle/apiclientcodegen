using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using Moq;
using Rapicgen.CLI.Commands.CSharp;
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
        RefitterCommand sut)
    {
        // Create a temporary file to use as settings file
        var tempFile = Path.GetTempFileName();
        try
        {
            sut.SettingsFile = tempFile;
            sut.OnExecute();
            
            // Verify that SwaggerFile was updated to match SettingsFile
            Assert.Equal(tempFile, sut.SwaggerFile);
            
            Mock.Get(factory)
                .Verify(
                    f => f.Create(
                        tempFile,
                        sut.DefaultNamespace,
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