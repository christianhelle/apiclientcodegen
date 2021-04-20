using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    public class ActionExtensionsTests
    {
        [Xunit.Fact]
        public void SafeInvoke_Invokes_Action()
        {
            var actionInvoked = false;
            new Action(() => actionInvoked = true).SafeInvoke();
            actionInvoked.Should().BeTrue();
        }

        [Xunit.Fact]
        public void SafeInvoke_Swallows_Exceptions()
            => new Action(
                    () => new Action(
                            () => throw new Exception())
                        .SafeInvoke())
                .Should()
                .NotThrow<Exception>();
    }
}