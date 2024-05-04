using System;

namespace Rapicgen.Core.Logging
{
    public interface IRemoteLogger
    {
        void TrackFeatureUsage(string featureName, params string[] tags);
        void TrackError(Exception exception);

        void TrackDependency(
            string dependencyName,
            string? data = null,
            DateTimeOffset startTime = default,
            TimeSpan duration = default,
            bool success = false);

        void Disable();

        void Enable();

        void WriteLine(object data);
    }
}