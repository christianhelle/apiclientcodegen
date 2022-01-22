using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.ApiClientCodeGen.Tests
{

    [ZoneDefinition]
    public class ApiClientCodeGenTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IApiClientCodeGenZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<ApiClientCodeGenTestEnvironmentZone> { }
    
    [SetUpFixture]
    public class ApiClientCodeGenTestsAssembly : ExtensionTestEnvironmentAssembly<ApiClientCodeGenTestEnvironmentZone> { }
}