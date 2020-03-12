using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class ActionExtensionsTests
    {
        [TestMethod]
        public void SafeInvoke_Invokes_Action()
        {
            var actionInvoked = false;
            new Action(() => actionInvoked = true).SafeInvoke();
            actionInvoked.Should().BeTrue();
        }

        [TestMethod]
        public void SafeInvoke_Swallows_Exceptions()
            => new Action(() => throw new Exception())
                .SafeInvoke();
    }
}