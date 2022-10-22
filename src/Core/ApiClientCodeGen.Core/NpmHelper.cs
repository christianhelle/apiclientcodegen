using System;
using System.Diagnostics;
using System.IO;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core
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

        public static string? TryGetNpmPrefixPathFromNpmConfig(IProcessLauncher? processLauncher = null)
        {
            try
            {
                var npm = GetNpmPath();
                string prefix = null!;

                using var context = new DependencyContext("npm config get prefix");
                (processLauncher ?? new ProcessLauncher()).Start(
                    npm,
                    "config get prefix",
                    o => prefix += o,
                    e => Trace.WriteLine(e));
                context.Succeeded();
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