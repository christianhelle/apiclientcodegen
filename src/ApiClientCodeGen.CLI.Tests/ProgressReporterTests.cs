using System;
using ApiClientCodeGen.CLI.Logging;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests
{
    public class ProgressReporterTests
    {
        [Fact]
        public void Requires_IConsoleOutput()
            => new Action(() => new ProgressReporter(null))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Writes_To_IConsoleOutput(
            IConsoleOutput console,
            uint progress)
        {
            var output = $"PROGRESS: {progress}%";
            new ProgressReporter(console).Progress(progress);
            Mock.Get(console)
                .Verify(expression: c => c.WriteLine(output));
        }

        [Theory, AutoMoqData]
        public void Writes_To_IConsoleOutput_With_Total(
            IConsoleOutput console,
            uint progress,
            uint total)
        {
            var output = $"PROGRESS: {progress} / {total}";
            new ProgressReporter(console).Progress(progress, total);
            Mock.Get(console)
                .Verify(expression: c => c.WriteLine(output));
        }
    }
}