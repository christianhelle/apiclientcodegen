﻿using System.Threading.Tasks;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;
using FluentAssertions;
using Moq;
using NJsonSchema.CodeGeneration.CSharp;

namespace Rapicgen.Tests.Generators.NSwag
{ 
    public class NSwagCodeGeneratorSettingsFactoryTests : TestWithResources
    {
        [Xunit.Fact]
        public async Task Does_Not_Return_Null_Async()
            => new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    Test.CreateDummy<INSwagOptions>())
                .GetGeneratorSettings(
                    await new OpenApiDocumentFactory()
                        .GetDocumentAsync(SwaggerJsonFilename))
                .Should()
                .NotBeNull();

        [Xunit.Fact]
        public async Task Returns_SystemTextJson_Library_When_UseSystemTextJson_Is_True()
        {
            var options = new Mock<INSwagOptions>();
            options.Setup(o => o.UseSystemTextJson).Returns(true);

            var settings = new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    options.Object)
                .GetGeneratorSettings(
                    await new OpenApiDocumentFactory()
                        .GetDocumentAsync(SwaggerJsonFilename));

            settings.CSharpGeneratorSettings.JsonLibrary.Should().Be(CSharpJsonLibrary.SystemTextJson);
        }

        [Xunit.Fact]
        public async Task Returns_NewtonsoftJson_Library_When_UseSystemTextJson_Is_False()
        {
            var options = new Mock<INSwagOptions>();
            options.Setup(o => o.UseSystemTextJson).Returns(false);

            var settings = new NSwagCodeGeneratorSettingsFactory(
                    Test.CreateAnnonymous<string>(),
                    options.Object)
                .GetGeneratorSettings(
                    await new OpenApiDocumentFactory()
                        .GetDocumentAsync(SwaggerJsonFilename));

            settings.CSharpGeneratorSettings.JsonLibrary.Should().Be(CSharpJsonLibrary.NewtonsoftJson);
        }
    }
}