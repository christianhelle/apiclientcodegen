﻿using System.IO;
using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Options.NSwagStudio;
using Moq;

namespace ApiClientCodeGen.Core.Tests.Generators.NSwagStudio
{
    public class NSwagStudioFileHelperTests : TestWithResources
    {
        private Mock<INSwagStudioOptions> mock;

        protected override async Task OnInitializeAsync()
        {
            mock = new Mock<INSwagStudioOptions>();

            await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    new EnterOpenApiSpecDialogResult(
                        File.ReadAllText(SwaggerJsonFilename),
                        "Swagger",
                        "https://petstore.swagger.io/v2/swagger.json"),
                    mock.Object);
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
