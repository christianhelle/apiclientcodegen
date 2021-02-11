using ApiClientCodeGen.VSMac.Commands.NSwagStudio;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.VSMac.Tests
{
    public class ContainerTests
    {
        [Fact]
        public void Instance_NotNull()
            => Container.Instance.Should().NotBeNull();

        [Fact]
        public void CanResolve_GenerateNSwagStudioCommand()
            => Container.Instance
                .Resolve<GenerateNSwagStudioCommand>()
                .Should()
                .NotBeNull();
    }
}