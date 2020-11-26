using System;
using System.IO;

namespace ApiClientCodeGen.Tests.Common.Resources
{
    public static class EmbeddedResources
    {
        private static readonly Type Type = typeof(EmbeddedResources);
        public static Stream GetStream(string name)
            => Type.Assembly.GetManifestResourceStream(Type, name);
    }
}