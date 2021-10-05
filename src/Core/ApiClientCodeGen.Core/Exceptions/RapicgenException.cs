using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RapicgenException : Exception
    {
        public RapicgenException()
        {
        }

        public RapicgenException(string message) : base(message)
        {
        }

        public RapicgenException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RapicgenException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}