using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Extensions;

namespace Rapicgen.Core.Logging
{
    public static class SupportInformation
    {
        public static string GetSupportKey()
            => GetAnonymousIdentity().Substring(0, 7);

        public static string GetAnonymousIdentity()
            => $"{Environment.UserName}@{GetMachineName()}".ToSha256();

        [ExcludeFromCodeCoverage]
        private static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch
            {
                return "localhost";
            }
        }
    }
}