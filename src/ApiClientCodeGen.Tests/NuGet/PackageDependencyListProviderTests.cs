using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.NuGet;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class PackageDependencyListProviderTests
    {
        private readonly PackageDependencyListProvider sut
            = new PackageDependencyListProvider();

        [TestMethod]
        public void GetDependencies_NSwag_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_AutoRest_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_Swagger_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_OpenApi_Returns_NotEmpty() 
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_Swagger_OpenApi_Same_Dependencies()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .BeEquivalentTo(
                    sut.GetDependencies(
                        SupportedCodeGenerator.OpenApi));

        [TestMethod]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [TestMethod]
        public void GetDependencies_NSwag_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod]
        public void GetDependencies_NSwag_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod]
        public void GetDependencies_NSwagStudio_Contains_NewtonsoftJson()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.NewtonsoftJson);

        [TestMethod]
        public void GetDependencies_NSwagStudio_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod]
        public void GetDependencies_NSwagStudio_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwagStudio)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => sut.GetDependencies(SupportedCodeGenerator.AutoRest)
                .Should()
                .Contain(PackageDependencies.MicrosoftRestClientRuntime);

        [TestMethod]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [TestMethod]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => sut.GetDependencies(SupportedCodeGenerator.Swagger)
                .Should()
                .Contain(PackageDependencies.JsonSubTypes);

        [TestMethod]
        public void GetDependencies_Swagger_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod]
        public void GetDependencies_Swagger_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.NSwag)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_RestSharp()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.RestSharp);

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_JsonSubTypes()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.JsonSubTypes);

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_SystemRuntimeSerializationPrimitives()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemRuntimeSerializationPrimitives);

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_SystemComponentModelAnnotations()
            => sut.GetDependencies(SupportedCodeGenerator.OpenApi)
                .Should()
                .Contain(PackageDependencies.SystemComponentModelAnnotations);
    }
}
