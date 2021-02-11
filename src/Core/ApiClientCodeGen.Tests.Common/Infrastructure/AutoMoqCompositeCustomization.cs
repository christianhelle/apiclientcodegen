using AutoFixture;
using AutoFixture.AutoMoq;

namespace ApiClientCodeGen.Tests.Common.Infrastructure
{
    public class AutoMoqCompositeCustomization : CompositeCustomization
    {
        public AutoMoqCompositeCustomization()
            : base(new AutoMoqCustomization()) { }
    }
}