using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SystemTrace = System.Diagnostics.Trace;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public static class TraceLogger
    {
        private static readonly List<ITraceLogger> Loggers = new List<ITraceLogger>
        {
            new SystemTraceLogger()
        };

        public static void Setup(params ITraceLogger[] loggers)
        {
            Loggers.AddRange(loggers);
        }
        
        public static void Write(string message) 
            => Loggers.ForEach(c => c.WriteLine(message));

        public static void WriteLine(string message)
        => Loggers.ForEach(c => c.WriteLine(message));

        public static void Write(Exception exception)
            => Loggers.ForEach(c => c.Write(exception.ToString()));
    }

    public interface ITraceLogger
    {
        void Write(string message);
        void WriteLine(string message);
        void Write(Exception exception);
    }

    public class SystemTraceLogger : ITraceLogger
    {
        public void Write(string message)
            => SystemTrace.WriteLine(message);

        public void WriteLine(string message)
            => SystemTrace.WriteLine(message);

        public void Write(Exception exception)
            => SystemTrace.TraceError(exception.ToString());
    }
}