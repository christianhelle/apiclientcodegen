using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public static class SupportInformation
    {
        public static string GetShortSupportKey()
            => GetFullSupportKey().Substring(0, 7);

        public static string GetFullSupportKey()
            => Environment.UserName.ToSha256();
    }
}