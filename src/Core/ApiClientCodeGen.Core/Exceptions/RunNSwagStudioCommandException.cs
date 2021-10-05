using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RunNSwagStudioCommandException : RapicgenException
    {
        public RunNSwagStudioCommandException()
        {
        }

        public RunNSwagStudioCommandException(string message) : base(message)
        {
        }

        public RunNSwagStudioCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RunNSwagStudioCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}