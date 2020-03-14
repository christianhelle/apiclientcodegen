using System;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using MonoDevelop.Core;

namespace ApiClientCodeGen.VSMac
{
    [ExcludeFromCodeCoverage]
    public class ProgressReporter : IProgressReporter
    {
        private readonly ProgressMonitor monitor;

        public ProgressReporter(ProgressMonitor monitor)
        {
            this.monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
        }

        public void Progress(uint progress, uint total = 100)
        {
            monitor.Step((int)progress);
        }
    }
}