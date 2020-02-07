using System;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Moq;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests
{
    public class ProgressReporterTests
    {
        [Fact]
        public void Requires_IConsole()
            => new Action(() => new ProgressReporter(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Writes_To_IConsole(
            IConsole console,
            uint progress)
        {
            new ProgressReporter(console).Progress(progress);
            Mock.Get(console)
                .Verify(c => c.WriteLine($"PROGRESS: {progress} / 100"));
        }

        [Theory, AutoMoqData]
        public void Writes_To_IConsole_With_Total(
            IConsole console,
            uint progress,
            uint total)
        {
            new ProgressReporter(console).Progress(progress, total);
            Mock.Get(console)
                .Verify(c => c.WriteLine($"PROGRESS: {progress}%"));
        }
    }
}