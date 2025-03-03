using Rapicgen.Core;
using Rapicgen.Core.NuGet;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.NuGet
{
    public class PackageDependencyListProviderTests
    {
        private readonly PackageDependencyListProvider sut
            = new PackageDependencyListProvider();

        [Xunit.Fact]
        public void GetDependencies_NSwag_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_AutoRest_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_Swagger_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_Kiota_Returns_NotEmpty()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [Xunit.Fact]
        public void GetDependencies_NSwag_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [Xunit.Fact]
        public void GetDependencies_NSwag_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .Contain(PackageDependencies.MicrosoftRestClientRuntime);

        [Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.JsonSubTypes);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_MicrosoftCSharp()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.MicrosoftCSharp);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHttp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftExtensionsHttp);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHosting()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftExtensionsHosting);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHttpPolly()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftExtensionsHttpPolly);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemThreadingChannels()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemThreadingChannels);

        [Xunit.Fact]
        public void GetDependencies_AutoRestV3_Contains_RestClientRuntime()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRestV3)
                .Should()
                .Contain(PackageDependencies.MicrosoftRestClientRuntime);

        [Xunit.Fact]
        public void GetDependencies_AutoRestV3_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRestV3)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [Xunit.Fact]
        public void GetDependencies_AutoRestV3_Contains_AutoRestCSharp()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRestV3)
                .Should()
                .Contain(PackageDependencies.AutoRestCSharp);

        [Xunit.Fact]
        public void GetDependencies_AutoRestV3_Contains_AzureCore()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRestV3)
                .Should()
                .Contain(PackageDependencies.AzureCore);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_AzureIdentity()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.AzureIdentity);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaAbstractions()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaAbstractions);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaAuthenticationAzure()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaAuthenticationAzure);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaHttpClientLibrary()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaHttpClientLibrary);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaSerializationForm()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaSerializationForm);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaSerializationJson()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaSerializationForm);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaSerializationText()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaSerializationText);

        [Xunit.Fact]
        public void GetDependencies_Kiota_Contains_MicrosoftKiotaSerializationMultipart()
            => sut.GetDependencies(SupportedCodeGenerator.Kiota)
                .Should()
                .Contain(PackageDependencies.MicrosoftKiotaSerializationMultipart);

        [Xunit.Fact]
        public void GetDependencies_Refitter_Contains_Refit()
            => sut.GetDependencies(SupportedCodeGenerator.Refitter)
                .Should()
                .Contain(PackageDependencies.Refit);
    }
}
