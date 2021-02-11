using System;

namespace ApiClientCodeGen.VSMac.Logging
{
    public interface ILoggingService : IDisposable
    {
        void Log(string message);
    }
}