using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    [TestClass]
    public class StringToIntPtrTests
    {
        private string str;
        private IntPtr result;
        private uint length;

        [TestInitialize]
        public void Init()
        {
            str = Test.CreateAnnonymous<string>();
            result = str.ConvertToIntPtr(out length);
        }

        [TestMethod]
        public void IntPtr_Not_Zero()
            => result.Should().NotBe(IntPtr.Zero);

        [TestMethod]
        public void Length_Not_Zero()
            => length.Should().BeGreaterThan(0);

        [TestMethod]
        public void Length_Matches_String_Length()
            => length.Should().Be((uint)str.Length);
    }
}
