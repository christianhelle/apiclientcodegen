using System;
using ApiClientCodeGen.Tests.Common.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.Logging
{
    public class ProgressReporterTests
    {
        [Theory, AutoMoqData]
        public void Requires_IConsoleOutput(IVerboseOptions options)
            => new Action(() => new ProgressReporter(null, options))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        
        [Theory, AutoMoqData]
        public void Requires_IVerboseOptions(IConsoleOutput output)
            => new Action(() => new ProgressReporter(output, null))
                .Should()
                .ThrowExactly<ArgumentNullException>();

        [Theory, AutoMoqData]
        public void Writes_To_IConsoleOutput(
            IConsoleOutput console,
            IVerboseOptions options,
            uint progress)
        {
            var output = $"PROGRESS: {progress}%";
            new ProgressReporter(console, options).Progress(progress);
            Mock.Get(console)
                .Verify(expression: c => c.WriteLine(output));
        }

        [Theory, AutoMoqData]
        public void Writes_To_IConsoleOutput_With_Total(
            IConsoleOutput console,
            IVerboseOptions options,
            uint progress,
            uint total)
        {
            var output = $"PROGRESS: {progress} / {total}";
            new ProgressReporter(console, options).Progress(progress, total);
            Mock.Get(console)
                .Verify(expression: c => c.WriteLine(output));
        }
    }
}