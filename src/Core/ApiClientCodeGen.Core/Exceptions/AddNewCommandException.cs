using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rapicgen.Core.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class AddNewCommandException : RapicgenException
    {
        public AddNewCommandException()
        {
        }

        public AddNewCommandException(string message) : base(message)
        {
        }

        public AddNewCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AddNewCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}