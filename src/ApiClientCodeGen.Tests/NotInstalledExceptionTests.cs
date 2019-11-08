using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class NotInstalledExceptionTests
    {
        private readonly string message = Test.CreateAnnonymous<string>();

        [TestMethod]
        public void Can_Set_Message()
            => new NotInstalledException(message)
                .Message
                .Should()
                .Be(message);
    }
}
