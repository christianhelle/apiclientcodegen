using Rapicgen.Core.Logging;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class SupportInformationTests
    {
        [Fact]
        public void GetSupportKey_Returns_NotNullOrWhitespace()
            => SupportInformation.GetSupportKey()
                .Should()
                .NotBeNullOrWhiteSpace();

        [Fact]
        public void GetAnonymousName_Returns_NotNullOrWhitespace()
            => SupportInformation.GetAnonymousIdentity()
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}