using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    [ExcludeFromCodeCoverage]
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