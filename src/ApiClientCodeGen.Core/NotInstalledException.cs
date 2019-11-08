using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    [Serializable]
    public class NotInstalledException : Exception
    {
        public NotInstalledException(string message) : base(message) { }

        protected NotInstalledException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
