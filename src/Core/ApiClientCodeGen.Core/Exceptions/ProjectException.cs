using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ProjectException : CodeGeneratorException
    {
        public ProjectException()
        {
        }

        public ProjectException(string message) : base(message)
        {
        }

        public ProjectException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ProjectException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}