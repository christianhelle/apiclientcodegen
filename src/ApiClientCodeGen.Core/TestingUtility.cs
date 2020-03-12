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
        }

        private static bool IsTestFramework(Assembly assembly) 
            => assembly.FullName.Contains("Xunit") 
            || assembly.FullName.Contains("Microsoft.VisualStudio.TestTools.UnitTesting");

        public static bool IsRunningFromUnitTest { get; }
    }
}