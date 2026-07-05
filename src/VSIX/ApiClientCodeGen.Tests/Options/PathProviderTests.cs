using System;
using ApiClientCodeGen.Tests.Common;
using FluentAssertions;
using Rapicgen.Core.External;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Tests.Options
{
    public class PathProviderTests
    {
        [Xunit.Fact]
        public void GetJavaPath_Exists()
        {
            var path = PathProvider.GetInstalledJavaPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Xunit.Fact]
        public void GetJavaPath_Returns_No_Path_For_Bad_EnvironmentVariableName()
        {
            var path = PathProvider.GetInstalledJavaPath(Test.CreateAnnonymous<string>());
            path.Should().Be("java");
        }

        [Xunit.Fact]
        public void GetNpmPath_Exists()
        {
            var path = PathProvider.GetNpmPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Xunit.Fact]
        public void GetNpmPath_Exists_For_Specified_Paths()
        {
            var path = PathProvider.GetNpmPath(Test.CreateAnnonymous<string>(), Test.CreateAnnonymous<string>());
            path.Should().Be(Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix ? "npm" : string.Empty);
        }

        [Xunit.Fact]
        public void GetNSwagPath_Exists()
        {
            var path = PathProvider.GetNSwagPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Xunit.Fact]
        public void GetNSwagPath_Without_Path_Returns_nswag()
        {
            var path = PathProvider.GetNSwagPath(true);
            path.Should().Be("nswag");
        }
    }
}
