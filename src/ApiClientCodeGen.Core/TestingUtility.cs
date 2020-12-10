using System;
using System.Diagnostics;
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            IsRunningFromUnitTest = assemblies.Any(IsTestFramework);
        }

        private static bool IsTestFramework(Assembly assembly) 
            => assembly.FullName.Contains("Xunit");

        public static bool IsRunningFromUnitTest { get; }
    }
}