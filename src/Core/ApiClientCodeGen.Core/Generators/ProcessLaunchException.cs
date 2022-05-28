using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ProcessLaunchException : Exception
    {
        public string Command { get; } = null!;
        public string Arguments { get; } = null!;
        public string? WorkingDirectory { get; } = null!;
        public string OutputData { get; } = null!;
        public string ErrorData { get; } = null!;

        public ProcessLaunchException(string command, string arguments, string? workingDirectory, string outputData, string errorData)
            : base($"{command} failed!{Environment.NewLine}Output:{Environment.NewLine}{outputData}{Environment.NewLine}Error:{Environment.NewLine}{errorData}")
        {
            Command = command;
            Arguments = arguments;
            WorkingDirectory = workingDirectory;
            OutputData = outputData;
            ErrorData = errorData;
        }

        protected ProcessLaunchException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}