using System;

namespace Rapicgen.Core.Exceptions;

public class MissingJavaRuntimeException : Exception
{
    public MissingJavaRuntimeException() { }
    public MissingJavaRuntimeException(string message) : base(message) { }
    public MissingJavaRuntimeException(string message, Exception inner) : base(message, inner) { }
}