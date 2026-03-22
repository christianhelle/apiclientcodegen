using System;
using Xunit;

namespace ApiClientCodeGen.Tests.Common
{
    public abstract class AutoRestDeprecatedTestClass
    {
        protected AutoRestDeprecatedTestClass()
        {
            throw new AutoRestDeprecatedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoRestDeprecatedFactAttribute : FactAttribute
    {
        public AutoRestDeprecatedFactAttribute()
        {
            Skip = "AutoRest is deprecated and will be officially retired in July 2026";
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutoRestDeprecatedTheoryAttribute : TheoryAttribute
    {
        public AutoRestDeprecatedTheoryAttribute()
        {
            Skip = "AutoRest is deprecated and will be officially retired in July 2026";
        }
    }
}
