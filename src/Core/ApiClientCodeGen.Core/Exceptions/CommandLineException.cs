using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CommandLineException : RapicgenException
    {
        public CommandLineException()
        {
        }

        public CommandLineException(string message) : base(message)
        {
        }

        public CommandLineException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CommandLineException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}