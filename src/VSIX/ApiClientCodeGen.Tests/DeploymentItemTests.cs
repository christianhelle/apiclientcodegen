using System.IO;
using ApiClientCodeGen.Tests.Common;
using FluentAssertions;

namespace Rapicgen.Tests
{
    public class DeploymentItemTests : TestWithResources
    {        
        [Xunit.Fact]
        public void Can_Read_Test_Swagger_Spec()
            => File.ReadAllText(SwaggerJsonFilename)
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
