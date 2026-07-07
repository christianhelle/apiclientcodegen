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
