using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class PathProviderTests
    {
        [TestMethod, Xunit.Fact]
        public void GetJavaPath_Exists()
        {
            var path = PathProvider.GetJavaPath();
            path.Should().NotBeNullOrWhiteSpace();
        }
        
        [TestMethod, Xunit.Fact]
        public void GetJavaPath_Returns_No_Path_For_Bad_EnvironmentVariableName()
        {
            var path = PathProvider.GetJavaPath(Test.CreateAnnonymous<string>());
            path.Should().Be("java");
        }

        [TestMethod, Xunit.Fact]
        public void GetNpmPath_Exists()
        {
             var path = PathProvider.GetNpmPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod, Xunit.Fact]
        public void GetNpmPath_Exists_For_Specified_Paths()
        {
            var path = PathProvider.GetNpmPath(
                Test.CreateAnnonymous<string>(),
                Test.CreateAnnonymous<string>());
            path.Should().BeNull();
        }

        [TestMethod, Xunit.Fact]
        public void GetNSwagPath_Exists()
        {
            var path = PathProvider.GetNSwagPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod, Xunit.Fact]
        public void GetAutoRestPath_Returns_NpmPrefix_AutoRestCmd()
        {
            var path = PathProvider.GetAutoRestPath();
            path.Should().EndWith("autorest.cmd");
        }

        [TestMethod, Xunit.Fact]
        public void GetAutoRestPath_Without_Path_Returns_autorest()
        {
            var path = PathProvider.GetAutoRestPath(true);
            path.Should().Be("autorest");
        }

        [TestMethod, Xunit.Fact]
        public void GetNSwagPath_Without_Path_Returns_nswag()
        {
            var path = PathProvider.GetNSwagPath(true);
            path.Should().Be("nswag");
        }
    }
}
