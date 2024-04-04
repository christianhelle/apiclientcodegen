using System;

public class MissingJavaRuntimeException : Exception
{
    public MissingJavaRuntimeException() { }
    public MissingJavaRuntimeException(string message) : base(message) { }
    public MissingJavaRuntimeException(string message, Exception inner) : base(message, inner) { }
}