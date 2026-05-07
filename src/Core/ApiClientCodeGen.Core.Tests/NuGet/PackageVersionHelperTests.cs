using FluentAssertions;
using Rapicgen.Core.NuGet;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.NuGet
{
    public class PackageVersionHelperTests
    {
        [Theory]
        [InlineData("4.7.0", "4.5.0", true)]
        [InlineData("4.5.0", "4.5.0", true)]
        [InlineData("4.4.0", "4.5.0", false)]
        [InlineData("9.0.2", "9.0.2", true)]
        [InlineData("1.0.0", "2.0.0", false)]
        [InlineData("10.0.0", "9.0.0", true)]
        [InlineData("1.2.3.4", "1.2.3.0", true)]
        public void IsVersionGreaterOrEqual_Returns_Expected(
            string installed,
            string required,
            bool expected)
        {
            PackageVersionHelper
                .IsVersionGreaterOrEqual(installed, required)
                .Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("3.0.0-beta.20210218.1", "3.0.0", false)]
        [InlineData("5.0.0-rc.1", "5.0.0", false)]
        [InlineData("2.0.0-alpha", "3.0.0", false)]
        [InlineData("3.0.0-alpha", "3.0.0-beta.20210218.1", false)]
        public void IsVersionGreaterOrEqual_Handles_PreRelease(
            string installed,
            string required,
            bool expected)
        {
            PackageVersionHelper
                .IsVersionGreaterOrEqual(installed, required)
                .Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("not-a-version", "1.0.0")]
        [InlineData("1.0.0", "not-a-version")]
        [InlineData("", "1.0.0")]
        [InlineData("1.0.0", "")]
        public void IsVersionGreaterOrEqual_Returns_False_For_Invalid_Versions(
            string installed,
            string required)
        {
            PackageVersionHelper
                .IsVersionGreaterOrEqual(installed, required)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData("3.0.0-beta.20210218.1", "3.0.0")]
        [InlineData("5.0.0-rc.1", "5.0.0")]
        [InlineData("1.0.0", "1.0.0")]
        [InlineData("2.0.0-alpha", "2.0.0")]
        public void NormalizeVersion_Strips_PreRelease_Suffix(
            string input,
            string expected)
        {
            PackageVersionHelper
                .NormalizeVersion(input)
                .Should()
                .Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void NormalizeVersion_Returns_Input_For_Empty_Or_Null(string input)
        {
            PackageVersionHelper
                .NormalizeVersion(input)
                .Should()
                .Be(input);
        }
    }
}
