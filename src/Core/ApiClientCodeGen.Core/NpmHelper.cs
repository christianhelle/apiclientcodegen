using System;
using System.Diagnostics;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class NpmHelper
    {
        public static string GetNpmPath(bool withoutPath = false)
            => PathProvider.GetNpmPath(withoutPath: withoutPath);

        public static string GetPrefixPath()
            => TryGetNpmPrefixPathFromNpmConfig() ??
               Path.Combine(
                   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                   "npm");

        public static string TryGetNpmPrefixPathFromNpmConfig(IProcessLauncher processLauncher = null)
        {
            try
            {
                var npm = GetNpmPath();
                string prefix = null;
                (processLauncher ?? new ProcessLauncher()).Start(
                    npm,
                    "config get prefix",
                    o => prefix += o,
                    e => Trace.WriteLine(e));
                return prefix;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
                return null;
            }
        }
    }
}