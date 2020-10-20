using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using FluentAssertions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Extensions
{
    public class StringToIntPtrTests
    {
        private readonly string str;
        private readonly IntPtr result;
        private readonly uint length;

        public StringToIntPtrTests()
        {
            str = Test.CreateAnnonymous<string>();
            result = str.ConvertToIntPtr(out length);
        }

        [Xunit.Fact]
        public void IntPtr_Not_Zero()
            => result.Should().NotBe(IntPtr.Zero);

        [Xunit.Fact]
        public void Length_Not_Zero()
            => length.Should().BeGreaterThan(0);

        [Xunit.Fact]
        public void Length_Matches_String_Length()
            => length.Should().Be((uint)str.Length);
    }
}
