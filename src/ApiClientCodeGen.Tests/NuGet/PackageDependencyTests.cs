using System;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.NuGet;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class PackageDependencyTests
    {
        private string name;
        private Version version;
        private bool forceUpdate;
        private PackageDependency sut;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            name = fixture.Create<string>();
            version = fixture.Create<Version>();
            forceUpdate = fixture.Create<bool>();
            sut = new PackageDependency(name, version, forceUpdate);
        }

        [TestMethod]
        public void Equals_Compares_Values()
            => sut.Equals(
                    new PackageDependency(sut.Name, sut.Version, sut.ForceUpdate))
                .Should()
                .BeTrue();

        [TestMethod]
        public void Name_Set()
            => sut.Name.Should().Be(name);

        [TestMethod]
        public void Version_Set()
            => sut.Version.Should().Be(version);

        [TestMethod]
        public void ForceUpdate_Set()
            => sut.ForceUpdate.Should().Be(forceUpdate);

        [TestMethod]
        public void Name_NotBeNullOrWhiteSpace()
            => sut.Name.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void Version_NotBeNull()
            => sut.Version.Should().NotBeNull();
    }
}
