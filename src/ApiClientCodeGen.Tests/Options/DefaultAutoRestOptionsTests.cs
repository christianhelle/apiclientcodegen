using System;
using System.Dynamic;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class DefaultAutoRestOptionsTests
    {
        private IAutoRestOptions sut = new DefaultAutoRestOptions();

        [TestMethod]
        public void AddCredentials_Be_False()
            => sut.AddCredentials.Should().BeFalse();

        [TestMethod]
        public void SyncMethods_Be_Essential()
            => sut.SyncMethods.Should().Be(SyncMethodOptions.Essential);

        [TestMethod]
        public void ClientSideValidation_Be_False()
            => sut.ClientSideValidation.Should().BeTrue();

        [TestMethod]
        public void OverrideClientName_Be_False()
            => sut.OverrideClientName.Should().BeFalse();

        [TestMethod]
        public void UseInternalConstructors_Be_False()
            => sut.UseInternalConstructors.Should().BeFalse();

        [TestMethod]
        public void UseDateTimeOffset_Be_False()
            => sut.UseDateTimeOffset.Should().BeFalse();
    }
}