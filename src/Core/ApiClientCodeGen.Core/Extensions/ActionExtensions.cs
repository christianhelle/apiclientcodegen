using System;
using System.Diagnostics;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
            }
        }
    }
}