using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.AutoRest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class AutoRestOptionsTests
    {
        private IAutoRestOptions options;

        [TestInitialize]
        public void Init()
            => options = new Mock<IAutoRestOptions>().Object;

        [TestMethod]
        public void Reads_AddCredentials_From_Options()
            => new AutoRestOptions(options)
                .AddCredentials
                .Should()
                .Be(options.AddCredentials);

        [TestMethod]
        public void Reads_ClientSideValidation_From_Options()
            => new AutoRestOptions(options)
                .ClientSideValidation
                .Should()
                .Be(options.ClientSideValidation);

        [TestMethod]
        public void Reads_SyncMethods_From_Options()
            => new AutoRestOptions(options)
                .SyncMethods
                .Should()
                .Be(options.SyncMethods);

        [TestMethod]
        public void Reads_UseDateTimeOffset_From_Options()
            => new AutoRestOptions(options)
                .UseDateTimeOffset
                .Should()
                .Be(options.UseDateTimeOffset);

        [TestMethod]
        public void Reads_UseInternalConstructors_From_Options()
            => new AutoRestOptions(options)
                .UseInternalConstructors
                .Should()
                .Be(options.UseInternalConstructors);

        [TestMethod]
        public void Reads_OverrideClientName_From_Options()
            => new AutoRestOptions(options)
                .OverrideClientName
                .Should()
                .Be(options.OverrideClientName);
    }
}
