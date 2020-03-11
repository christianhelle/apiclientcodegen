using AutoFixture;
using AutoFixture.AutoMoq;

namespace ApiClientCodeGen.VSMac.Tests.Infrastructure
{
    public class AutoMoqCompositeCustomization : CompositeCustomization
    {
        public AutoMoqCompositeCustomization()
            : base(new AutoMoqCustomization()) { }
    }
}