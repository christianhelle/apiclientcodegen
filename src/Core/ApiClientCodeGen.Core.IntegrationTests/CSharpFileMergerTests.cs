﻿using System.IO;
using Rapicgen.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [Fact]
        public void Can_Merge_CSharp_Files()
        {
            var folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            CSharpFileMerger.MergeFiles(
                    folder)
                .Should()
                .NotBeNullOrWhiteSpace(folder);
        }
    }
}
