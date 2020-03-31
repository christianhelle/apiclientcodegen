using AutoFixture;
using Moq;

namespace ApiClientCodeGen.Core.Tests
{
    public static class Test
    {
        public static T CreateDummy<T>() where T : class => new Mock<T>().Object;
        
        public static T CreateAnnonymous<T>() where T : class => new Fixture().Create<T>();
    }
}
