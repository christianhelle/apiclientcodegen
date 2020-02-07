using AutoFixture;
using AutoFixture.AutoMoq;

namespace ApiClientCodeGen.CLI.Tests.Infrastructure
{
    public class AutoMoqCompositeCustomization : CompositeCustomization
    {
        public AutoMoqCompositeCustomization()
            : base(new AutoMoqCustomization()) { }
    }
}