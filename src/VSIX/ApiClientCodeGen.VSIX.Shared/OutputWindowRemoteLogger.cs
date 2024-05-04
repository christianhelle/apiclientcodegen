using Rapicgen.Core.Logging;
using Rapicgen.Windows;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Rapicgen
{
    [ExcludeFromCodeCoverage]
    public class OutputWindowRemoteLogger : IRemoteLogger
    {
        public void Disable()
        {
            // Method intentionally left empty.
        }

        public void Enable()
        {
            // Method intentionally left empty.
        }

        public void TrackDependency(
            string dependencyName,
            string data = null,
            DateTimeOffset startTime = default,
            TimeSpan duration = default,
            bool success = false)
        {
            // Method intentionally left empty.
        }

        public void TrackError(Exception exception)
        {
            WriteLine(exception);
        }

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            // Method intentionally left empty.
        }

        public void WriteLine(object data)
        {
            OutputWindow.Log(data);
        }
    }
}