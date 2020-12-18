using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class SupportInformationTests
    {
        [Fact]
        public void GetFullSupportKey_Returns_NotNullOrWhitespace()
            => SupportInformation.GetFullSupportKey()
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void GetAnnonymousName_Returns_NotNullOrWhitespace()
            => SupportInformation.GetAnnonymousName()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}