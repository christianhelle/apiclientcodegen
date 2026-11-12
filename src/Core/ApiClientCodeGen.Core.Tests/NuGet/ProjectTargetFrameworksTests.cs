using FluentAssertions;
using Rapicgen.Core.NuGet;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.NuGet
{
    public class ProjectTargetFrameworksTests
    {
        [Fact]
        public void Parse_Reads_TargetFramework()
            => ProjectTargetFrameworks
                .Parse("<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net8.0</TargetFramework></PropertyGroup></Project>")
                .Should()
                .BeEquivalentTo("net8.0");

        [Fact]
        public void Parse_Reads_TargetFrameworks()
            => ProjectTargetFrameworks
                .Parse("<Project><PropertyGroup><TargetFrameworks>netstandard2.0; net8.0</TargetFrameworks></PropertyGroup></Project>")
                .Should()
                .BeEquivalentTo("netstandard2.0", "net8.0");

        [Fact]
        public void Parse_Reads_Legacy_TargetFrameworkVersion()
            => ProjectTargetFrameworks
                .Parse("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\"><PropertyGroup><TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion></PropertyGroup></Project>")
                .Should()
                .BeEquivalentTo("net472");

        [Theory]
        [InlineData("")]
        [InlineData("not xml")]
        [InlineData("<Project />")]
        public void Parse_Returns_Empty_When_Unknown(string contents)
            => ProjectTargetFrameworks
                .Parse(contents)
                .Should()
                .BeEmpty();

        [Fact]
        public void Parse_Keeps_Unresolved_Property_Entries()
            => ProjectTargetFrameworks
                .Parse("<Project><PropertyGroup><TargetFrameworks>net8.0;$(LegacyTfm)</TargetFrameworks></PropertyGroup></Project>")
                .Should()
                .BeEquivalentTo("net8.0", "$(LegacyTfm)");

        [Fact]
        public void AreSupportedByLatestRefit_Returns_False_When_Any_Entry_Is_Unresolved()
            => ProjectTargetFrameworks
                .AreSupportedByLatestRefit(
                    ProjectTargetFrameworks.Parse(
                        "<Project><PropertyGroup><TargetFrameworks>net8.0;$(LegacyTfm)</TargetFrameworks></PropertyGroup></Project>"))
                .Should()
                .BeFalse();

        [Theory]
        [InlineData("net8.0")]
        [InlineData("net9.0")]
        [InlineData("net10.0")]
        [InlineData("net8.0-windows")]
        [InlineData("net462")]
        [InlineData("net47")]
        [InlineData("net472")]
        [InlineData("net481")]
        [InlineData("NET8.0")]
        public void IsSupportedByLatestRefit_Returns_True(string targetFramework)
            => ProjectTargetFrameworks
                .IsSupportedByLatestRefit(targetFramework)
                .Should()
                .BeTrue();

        [Theory]
        [InlineData("netstandard2.0")]
        [InlineData("netstandard2.1")]
        [InlineData("netcoreapp3.1")]
        [InlineData("net5.0")]
        [InlineData("net6.0")]
        [InlineData("net7.0")]
        [InlineData("net461")]
        [InlineData("net45")]
        [InlineData("")]
        [InlineData("garbage")]
        public void IsSupportedByLatestRefit_Returns_False(string targetFramework)
            => ProjectTargetFrameworks
                .IsSupportedByLatestRefit(targetFramework)
                .Should()
                .BeFalse();

        [Fact]
        public void AreSupportedByLatestRefit_Requires_All_Frameworks()
            => ProjectTargetFrameworks
                .AreSupportedByLatestRefit(new[] { "net8.0", "netstandard2.0" })
                .Should()
                .BeFalse();

        [Fact]
        public void AreSupportedByLatestRefit_Returns_True_When_All_Supported()
            => ProjectTargetFrameworks
                .AreSupportedByLatestRefit(new[] { "net8.0", "net472" })
                .Should()
                .BeTrue();

        [Fact]
        public void AreSupportedByLatestRefit_Returns_False_When_Empty()
            => ProjectTargetFrameworks
                .AreSupportedByLatestRefit(new string[0])
                .Should()
                .BeFalse();
    }
}
