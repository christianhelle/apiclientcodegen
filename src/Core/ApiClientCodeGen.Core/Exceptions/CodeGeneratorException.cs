using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CodeGeneratorException : RapicgenException
    {
        public CodeGeneratorException()
        {
        }

        public CodeGeneratorException(string message) : base(message)
        {
        }

        public CodeGeneratorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CodeGeneratorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}