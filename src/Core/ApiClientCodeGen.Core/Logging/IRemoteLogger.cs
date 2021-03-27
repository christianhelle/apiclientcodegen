using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public interface IRemoteLogger
    {
        void TrackFeatureUsage(string featureName, params string[] tags);
        void TrackError(Exception exception);
        void Disable();
    }
}