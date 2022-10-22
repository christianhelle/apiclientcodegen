using Rapicgen.Core.Options.AutoRest;
using FluentAssertions;

namespace Rapicgen.Tests.Options
{
    
    public class DefaultAutoRestOptionsTests
    {
        private readonly IAutoRestOptions sut = new DefaultAutoRestOptions();

        [Xunit.Fact]
        public void AddCredentials_Be_False()
            => sut.AddCredentials.Should().BeFalse();

        [Xunit.Fact]
        public void SyncMethods_Be_Essential()
            => sut.SyncMethods.Should().Be(SyncMethodOptions.Essential);

        [Xunit.Fact]
        public void ClientSideValidation_Be_False()
            => sut.ClientSideValidation.Should().BeTrue();

        [Xunit.Fact]
        public void OverrideClientName_Be_False()
            => sut.OverrideClientName.Should().BeFalse();

        [Xunit.Fact]
        public void UseInternalConstructors_Be_False()
            => sut.UseInternalConstructors.Should().BeFalse();

        [Xunit.Fact]
        public void UseDateTimeOffset_Be_False()
            => sut.UseDateTimeOffset.Should().BeFalse();
    }
}