using System;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Rapicgen.Core.External;

namespace Rapicgen.Tests.Options
{
    
    public class PathProviderTests
    {
        [Xunit.Fact]
        public void GetJavaPath_Exists()
        {
            var path = PathProvider.GetJavaPath();
            path.Should().NotBeNullOrWhiteSpace();
        }
        
        [Xunit.Fact]
        public void GetJavaPath_Returns_No_Path_For_Bad_EnvironmentVariableName()
        {
            var path = PathProvider.GetJavaPath(Test.CreateAnnonymous<string>());
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
            var path = PathProvider.GetNpmPath(
                Test.CreateAnnonymous<string>(),
                Test.CreateAnnonymous<string>());

            path.Should()
                .Be(
                    Environment.OSVersion.Platform is PlatformID.MacOSX or PlatformID.Unix
                        ? "npm"
                        : string.Empty);
        }

        [Xunit.Fact]
        public void GetNSwagPath_Exists()
        {
            var path = PathProvider.GetNSwagPath();
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Xunit.Fact]
        public void GetAutoRestPath_Returns_NpmPrefix_AutoRestCmd()
        {
            var path = PathProvider.GetAutoRestPath();
            path.Should().ContainAny("autorest");
        }

        [Xunit.Fact]
        public void GetAutoRestPath_Without_Path_Returns_autorest()
        {
            var path = PathProvider.GetAutoRestPath(true);
            path.Should().Be("autorest");
        }

        [Xunit.Fact]
        public void GetNSwagPath_Without_Path_Returns_nswag()
        {
            var path = PathProvider.GetNSwagPath(true);
            path.Should().Be("nswag");
        }
    }
}
