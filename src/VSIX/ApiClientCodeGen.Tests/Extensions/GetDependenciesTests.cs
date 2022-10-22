﻿using System.Linq;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using FluentAssertions;

namespace Rapicgen.Tests.Extensions
{
    public class GetDependenciesTests
    {
        [Xunit.Fact]
        public void GetDependencies_AutoRest()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Any(c => c.Name == "Microsoft.Rest.ClientRuntime")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_NSwag()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Any(c => c.Name == "Newtonsoft.Json")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_Swagger()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Any(c => c.Name == "RestSharp" || c.Name == "JsonSubTypes")
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void GetDependencies_OpenApi()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Any(c => c.Name == "RestSharp" || c.Name == "JsonSubTypes")
                .Should()
                .BeTrue();
    }
}
