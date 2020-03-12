using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class DefaultNSwagStudioOptionsTests : DefaultNSwagOptionsTests
    {
        private INSwagStudioOptions sut;

        [TestInitialize]
        public void Init() => sut = Create() as INSwagStudioOptions;
            
        protected override INSwagOptions Create() => new DefaultNSwagStudioOptions();

        [TestMethod]
        public void GenerateDataAnnotations_BeFalse()
            => sut.GenerateDataAnnotations.Should().BeFalse();

        [TestMethod]
        public void GenerateDefaultValues_BeFalse()
            => sut.GenerateDefaultValues.Should().BeFalse();

        [TestMethod]
        public void GenerateJsonMethods_BeFalse()
            => sut.GenerateJsonMethods.Should().BeFalse();

        [TestMethod]
        public void GenerateResponseClasses_BeFalse()
            => sut.GenerateResponseClasses.Should().BeFalse();

        [TestMethod]
        public void RequiredPropertiesMustBeDefined_BeFalse()
            => sut.RequiredPropertiesMustBeDefined.Should().BeFalse();
    }
}