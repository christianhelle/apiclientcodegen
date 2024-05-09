using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RunKiotaCommandException : RapicgenException
    {
        public RunKiotaCommandException()
        {
        }

        public RunKiotaCommandException(string message) : base(message)
        {
        }

        public RunKiotaCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RunKiotaCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}