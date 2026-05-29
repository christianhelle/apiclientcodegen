using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.External
{
    /// <summary>
    /// Runs an external code-generation tool, concentrating the boilerplate every generator used to
    /// repeat by hand: wrapping the launch in a <see cref="DependencyContext"/> for telemetry, invoking
    /// the <see cref="IProcessLauncher"/>, and resolving the Java runtime for JAR-based tools.
    /// </summary>
    /// <remarks>
    /// Constructed internally by generators from their injected <see cref="IProcessLauncher"/> (the same
    /// way they construct <see cref="JavaPathProvider"/>), so the seam used by generator tests remains the
    /// process launcher.
    /// </remarks>
    public class ToolRunner
    {
        private readonly IProcessLauncher processLauncher;

        public ToolRunner(IProcessLauncher processLauncher)
        {
            this.processLauncher = processLauncher;
        }

        /// <summary>
        /// Launches <paramref name="command"/> with <paramref name="arguments"/>, recording a dependency
        /// telemetry span named after the tool and marking it succeeded once the process completes.
        /// </summary>
        public void Run(
            ExternalTool tool,
            string command,
            string arguments,
            string? workingDirectory = null)
        {
            using var context = new DependencyContext(tool.DisplayName, $"{command} {arguments}");
            processLauncher.Start(command, arguments, workingDirectory);
            context.Succeeded();
        }

        /// <summary>
        /// Resolves the Java runtime (honoring configured, bundled and installed locations) and runs the
        /// tool's JAR <paramref name="arguments"/> through it.
        /// </summary>
        public void RunJava(
            ExternalTool tool,
            IGeneralOptions options,
            string arguments,
            string? workingDirectory = null)
        {
            var java = new JavaPathProvider(options, processLauncher).GetJavaExePath();
            Run(tool, java, arguments, workingDirectory);
        }
    }
}
