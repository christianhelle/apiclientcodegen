using System;

namespace ApiClientCodeGen.Tests.Common
{
    public class AutoRestDeprecatedException : Exception
    {
        public AutoRestDeprecatedException()
            : base("AutoRest is deprecated and will be officially retired in July 2026. See: https://github.com/Azure/autorest/blob/main/docs/autorest-deprecated.md")
        {
        }

        public AutoRestDeprecatedException(string message)
            : base(message)
        {
        }

        public AutoRestDeprecatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
