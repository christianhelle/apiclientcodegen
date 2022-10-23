using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomToolCommandException : RapicgenException
    {
        public CustomToolCommandException()
        {
        }

        public CustomToolCommandException(string message) : base(message)
        {
        }

        public CustomToolCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CustomToolCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}