using System;
using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this System.Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
        }
    }
}