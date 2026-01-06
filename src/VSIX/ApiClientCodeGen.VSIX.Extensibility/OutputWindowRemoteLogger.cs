using Microsoft.VisualStudio.Extensibility.Documents;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.VSIX.Extensibility;

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

public class OutputWindowRemoteLogger(OutputChannel outputChannel) : IRemoteLogger
{
    private readonly object lockObject = new();

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
        string? data = null,
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
        lock (lockObject)
        {
            outputChannel.Writer.WriteLine(data);
        }
    }
}