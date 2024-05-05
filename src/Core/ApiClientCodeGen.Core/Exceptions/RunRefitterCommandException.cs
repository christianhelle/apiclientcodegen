using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RunRefitterCommandException : RapicgenException
    {
        public RunRefitterCommandException()
        {
        }

        public RunRefitterCommandException(string message) : base(message)
        {
        }

        public RunRefitterCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RunRefitterCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}