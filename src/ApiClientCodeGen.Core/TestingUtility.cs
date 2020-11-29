using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    [ExcludeFromCodeCoverage]
    public static class TestingUtility
    {
        static TestingUtility()
        {
            IsRunningFromUnitTest = AppDomain.CurrentDomain.GetAssemblies().Any(IsTestFramework);

            IsRunningFromIntegrationTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(asm => asm.FullName.Contains("IntegrationTests"));
        }

        private static bool IsTestFramework(Assembly assembly) 
            => assembly.FullName.Contains("Xunit") 
            || assembly.FullName.Contains("Test");

        public static bool IsRunningFromUnitTest { get; }

        public static bool IsRunningFromIntegrationTest { get; }
    }
}