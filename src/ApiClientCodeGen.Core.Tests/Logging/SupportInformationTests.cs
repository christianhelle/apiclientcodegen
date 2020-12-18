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
        public void GetShortSupportKey_Returns_NotNullOrWhitespace()
            => SupportInformation.GetShortSupportKey()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}