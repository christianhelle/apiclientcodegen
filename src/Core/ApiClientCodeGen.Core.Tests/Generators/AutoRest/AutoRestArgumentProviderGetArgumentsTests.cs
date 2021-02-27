using ApiClientCodeGen.Tests.Common.Infrastructure;
using AutoFixture.Xunit2;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.AutoRest
{
    public class AutoRestArgumentProviderGetArgumentsTests
    {
        [Theory, AutoMoqData]
        public void Reads_AddCredentials_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.AddCredentials, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_ClientSideValidation_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.ClientSideValidation, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_OverrideClientName_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.OverrideClientName, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_SyncMethods_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.SyncMethods, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_UseDateTimeOffset_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.UseDateTimeOffset, Times.AtLeastOnce);
        }

        [Theory, AutoMoqData]
        public void Reads_UseInternalConstructors_From_Options(
            [Frozen] IAutoRestOptions options,
            AutoRestArgumentProvider sut,
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            sut.GetArguments(outputFolder, swaggerFile, defaultNamespace);
            Mock.Get(options).Verify(c => c.UseInternalConstructors, Times.AtLeastOnce);
        }
    }
}