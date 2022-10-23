using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using MonoDevelop.Core;

namespace ApiClientCodeGen.VSMac.Logging
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