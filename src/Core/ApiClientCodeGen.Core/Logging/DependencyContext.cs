using System;
using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

public sealed class DependencyContext : IDisposable
{
    private readonly string dependencyName;
    private readonly string? data;
    private readonly Stopwatch stopwatch;
    private readonly DateTimeOffset timestamp;
    private bool success;

    public DependencyContext(string dependencyName, string? data = null, bool success = false)
    {
        this.dependencyName = dependencyName;
        this.data = data;
        this.success = success;
        timestamp = DateTimeOffset.UtcNow;
        stopwatch = Stopwatch.StartNew();
    }

    public void Succeeded() => success = true;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DependencyContext()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            Logger.Instance.TrackDependency(
                dependencyName,
                data,
                timestamp,
                stopwatch.Elapsed,
                success);
        }
    }
}