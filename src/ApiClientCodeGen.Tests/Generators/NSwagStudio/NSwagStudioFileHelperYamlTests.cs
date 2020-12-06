using System.IO;
using ApiClientCodeGen.Tests.Common;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    public class NSwagStudioFileHelperYamlTests : TestWithResources
    {
        private Mock<INSwagStudioOptions> mock;

        public NSwagStudioFileHelperYamlTests()
        {
            mock = new Mock<INSwagStudioOptions>();

            NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    new EnterOpenApiSpecDialogResult(
                        ReadAllText(SwaggerYaml),
                        "Swagger",
                        "https://petstore.swagger.io/v2/swagger.yaml"),
                    mock.Object)
                .GetAwaiter()
                .GetResult();
        }

        [Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => mock.Verify(c => c.InjectHttpClient);

        [Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => mock.Verify(c => c.GenerateClientInterfaces);

        [Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => mock.Verify(c => c.GenerateDtoTypes);

        [Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => mock.Verify(c => c.UseBaseUrl);

        [Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => mock.Verify(c => c.ClassStyle);

        [Xunit.Fact]
        public void Reads_GenerateResponseClasses_From_Options()
            => mock.Verify(c => c.GenerateResponseClasses);

        [Xunit.Fact]
        public void Reads_GenerateJsonMethods_From_Options()
            => mock.Verify(c => c.GenerateJsonMethods);

        [Xunit.Fact]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => mock.Verify(c => c.RequiredPropertiesMustBeDefined);

        [Xunit.Fact]
        public void Reads_GenerateDefaultValues_From_Options()
            => mock.Verify(c => c.GenerateDefaultValues);

        [Xunit.Fact]
        public void Reads_GenerateDataAnnotations_From_Options()
            => mock.Verify(c => c.GenerateDataAnnotations);
    }
}
