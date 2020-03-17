using System.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class PackageDependencyListProviderTests
    {
        private readonly PackageDependencyListProvider sut
            = new PackageDependencyListProvider();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .ToList()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .ToList()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .ToList()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .ToList()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .ToList()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_OpenApi_Same_Dependencies()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .BeEquivalentTo(
                    sut.GetDependencies(
                        SupportedCodeGenerator.OpenApi));

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwag_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .Contain(PackageDependencies.MicrosoftRestClientRuntime);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.JsonSubTypes);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_Swagger_Contains_MicrosoftCSharp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftCSharp);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_JsonSubTypes()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.JsonSubTypes);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod, Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftCSharp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.MicrosoftCSharp);
    }
}
