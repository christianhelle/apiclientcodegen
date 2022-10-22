using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomToolException : RapicgenException
    {
        public CustomToolException()
        {
        }

        public CustomToolException(string message) : base(message)
        {
        }

        public CustomToolException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CustomToolException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}