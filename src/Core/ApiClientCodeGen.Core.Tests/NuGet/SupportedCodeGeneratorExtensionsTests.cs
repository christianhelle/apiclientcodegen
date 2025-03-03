using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.NuGet
{
    
    public class SupportedCodeGeneratorExtensionsTests
    {
        [Xunit.Fact]
        public void GetDependencies_NSwag_Returns_NotEmpty()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Returns_NotEmpty()
            => SupportedCodeGenerator.NSwagStudio
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_AutoRest_Returns_NotEmpty()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_Swagger_Returns_NotEmpty()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Returns_NotEmpty()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [Xunit.Fact]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Newtonsoft.Json");

        [Xunit.Fact]
        public void GetDependencies_NSwagStudio_Contains_NewtonsoftJson()
            => SupportedCodeGenerator.NSwagStudio
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Newtonsoft.Json");

        [Xunit.Fact]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Rest.ClientRuntime");

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "RestSharp");

        [Xunit.Fact]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "JsonSubTypes");

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHosting()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Extensions.Hosting");

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHttp()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Extensions.Http");

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_MicrosoftExtensionsHttpPolly()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Extensions.Http.Polly");

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemThreadingChannels()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "System.Threading.Channels");

        [Xunit.Fact]
        public void GetDependencies_OpenApi_Contains_SystemComponentModelAnnotations()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "System.ComponentModel.Annotations");
        
        [Xunit.Fact]
        public void GetDependencies_Refitter_Returns_NotEmpty()
            => SupportedCodeGenerator.Refitter
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();
        
        [Xunit.Fact]
        public void GetDependencies_Refitter_Contains_Refit()
            => SupportedCodeGenerator.Refitter
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Refit");
    }
}
