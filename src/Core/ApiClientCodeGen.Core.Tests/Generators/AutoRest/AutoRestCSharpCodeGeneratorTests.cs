using ApiClientCodeGen.Tests.Common;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using Moq;
using NSwag;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Generators.AutoRest
{
    public class AutoRestCSharpCodeGeneratorTests : TestWithResources
    {
        private readonly Mock<IAutoRestOptions> optionsMock = new Mock<IAutoRestOptions>();
        private readonly Mock<IProgressReporter> progressMock = new Mock<IProgressReporter>();
        private readonly Mock<IProcessLauncher> processMock = new Mock<IProcessLauncher>();
        private readonly Mock<IOpenApiDocumentFactory> factoryMock = new Mock<IOpenApiDocumentFactory>();
        
        public AutoRestCSharpCodeGeneratorTests()
        {
            var document = OpenApiDocument.FromFileAsync(SwaggerJsonFilename).GetAwaiter().GetResult();
            factoryMock.Setup(c => c.GetDocumentAsync(It.IsAny<string>()))
                .ReturnsAsync(document);
            
            new AutoRestCSharpCodeGenerator(
                    "Swagger.json",
                    new Fixture().Create<string>(),
                    optionsMock.Object,
                    processMock.Object,
                    factoryMock.Object)
                .GenerateCode(progressMock.Object);
        }

        [Fact]
        public void Reads_AddCredentials_From_Options() 
            => optionsMock.Verify(c => c.AddCredentials, Times.AtLeastOnce);

        [Fact]
        public void Reads_ClientSideValidation_From_Options() 
            => optionsMock.Verify(c => c.ClientSideValidation, Times.AtLeastOnce);

        [Fact]
        public void Reads_OverrideClientName_From_Options() 
            => optionsMock.Verify(c => c.OverrideClientName, Times.AtLeastOnce);

        [Fact]
        public void Reads_SyncMethods_From_Options() 
            => optionsMock.Verify(c => c.SyncMethods, Times.AtLeastOnce);

        [Fact]
        public void Reads_UseDateTimeOffset_From_Options() 
            => optionsMock.Verify(c => c.UseDateTimeOffset, Times.AtLeastOnce);

        [Fact]
        public void Reads_UseInternalConstructors_From_Options() 
            => optionsMock.Verify(c => c.UseInternalConstructors, Times.AtLeastOnce);

        [Fact]
        public void Updates_Progress()
            => progressMock.Verify(
                c => c.Progress(
                    It.IsAny<uint>(),
                    It.IsAny<uint>()),
                Times.Exactly(5));
    }
}