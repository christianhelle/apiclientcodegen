using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging
{
    public static class SupportInformation
    {
        public static string GetAnnonymousName()
            => GetSupportKey().Substring(0, 7);

        public static string GetSupportKey()
            => Environment.UserName.ToSha256();
    }
}