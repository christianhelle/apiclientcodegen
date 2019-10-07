using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class PathProviderTests
    {
        [TestMethod]
        public void GetJavaPath_Exists()
        {
            var path = PathProvider.GetJavaPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void GetNpmPath_Exists()
        {
             var path = PathProvider.GetNpmPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void GetNSwagPath_Exists()
        {
            var path = PathProvider.GetNSwagPath();
            path.Should().NotBeNullOrWhiteSpace();
        }
    }
}
