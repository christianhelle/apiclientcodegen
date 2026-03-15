using FluentAssertions;
using Rapicgen.Core.NuGet;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.NuGet;

public class PackageDependenciesTests
{
    [Fact]
    public void NewtonsoftJson_HasCorrectName()
        => PackageDependencies.NewtonsoftJson.Name.Should().Be("Newtonsoft.Json");

    [Fact]
    public void NewtonsoftJson_IsNotForceUpdate()
        => PackageDependencies.NewtonsoftJson.ForceUpdate.Should().BeFalse();

    [Fact]
    public void RestSharp_HasCorrectName()
        => PackageDependencies.RestSharp.Name.Should().Be("RestSharp");

    [Fact]
    public void Refit_HasCorrectName()
        => PackageDependencies.Refit.Name.Should().Be("Refit");

    [Fact]
    public void AzureIdentity_HasCorrectName()
        => PackageDependencies.AzureIdentity.Name.Should().Be("Azure.Identity");

    [Fact]
    public void MicrosoftKiotaAbstractions_HasCorrectName()
        => PackageDependencies.MicrosoftKiotaAbstractions.Name
            .Should().Be("Microsoft.Kiota.Abstractions");

    [Fact]
    public void SystemRuntimeSerializationPrimitives_IsSystemLibrary()
        => PackageDependencies.SystemRuntimeSerializationPrimitives.IsSystemLibrary
            .Should().BeTrue();

    [Fact]
    public void SystemComponentModelAnnotations_IsSystemLibrary()
        => PackageDependencies.SystemComponentModelAnnotations.IsSystemLibrary
            .Should().BeTrue();

    [Fact]
    public void MicrosoftCSharp_IsSystemLibrary()
        => PackageDependencies.MicrosoftCSharp.IsSystemLibrary
            .Should().BeTrue();

    [Fact]
    public void MicrosoftRestClientRuntime_HasCorrectName()
        => PackageDependencies.MicrosoftRestClientRuntime.Name
            .Should().Be("Microsoft.Rest.ClientRuntime");

    [Fact]
    public void Polly_HasCorrectName()
        => PackageDependencies.Polly.Name.Should().Be("Polly");

    [Fact]
    public void RestSharpLatest_HasCorrectName()
        => PackageDependencies.RestSharpLatest.Name.Should().Be("RestSharp");

    [Fact]
    public void JsonSubTypes_HasCorrectName()
        => PackageDependencies.JsonSubTypes.Name.Should().Be("JsonSubTypes");

    [Fact]
    public void MicrosoftExtensionsHttp_HasCorrectName()
        => PackageDependencies.MicrosoftExtensionsHttp.Name
            .Should().Be("Microsoft.Extensions.Http");

    [Fact]
    public void MicrosoftExtensionsHosting_HasCorrectName()
        => PackageDependencies.MicrosoftExtensionsHosting.Name
            .Should().Be("Microsoft.Extensions.Hosting");

    [Fact]
    public void AllDependencies_HaveNonEmptyVersion()
    {
        var dependencies = new[]
        {
            PackageDependencies.NewtonsoftJson,
            PackageDependencies.MicrosoftRestClientRuntime,
            PackageDependencies.RestSharp,
            PackageDependencies.JsonSubTypes,
            PackageDependencies.RestSharpLatest,
            PackageDependencies.JsonSubTypesLatest,
            PackageDependencies.SystemRuntimeSerializationPrimitives,
            PackageDependencies.SystemComponentModelAnnotations,
            PackageDependencies.MicrosoftCSharp,
            PackageDependencies.Polly,
            PackageDependencies.AutoRestCSharp,
            PackageDependencies.AzureCore,
            PackageDependencies.AzureIdentity,
            PackageDependencies.MicrosoftKiotaAbstractions,
            PackageDependencies.MicrosoftKiotaAuthenticationAzure,
            PackageDependencies.MicrosoftKiotaHttpClientLibrary,
            PackageDependencies.MicrosoftKiotaSerializationForm,
            PackageDependencies.MicrosoftKiotaSerializationJson,
            PackageDependencies.MicrosoftKiotaSerializationText,
            PackageDependencies.MicrosoftKiotaSerializationMultipart,
            PackageDependencies.Refit,
            PackageDependencies.MicrosoftExtensionsHttp,
            PackageDependencies.MicrosoftExtensionsHosting,
            PackageDependencies.MicrosoftExtensionsHttpPolly,
            PackageDependencies.SystemThreadingChannels,
        };

        foreach (var dep in dependencies)
        {
            dep.Version.Should().NotBeNullOrWhiteSpace($"because {dep.Name} should have a version");
        }
    }
}
