namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build
{
    public class ProjectFileContents
    {
        public const string NetCoreApp =
@"<Project Sdk=\""Microsoft.NET.Sdk\"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = \""JsonSubTypes\"" Version=\""1.2.0\"" />
    <PackageReference Include = \""Microsoft.Rest.ClientRuntime\"" Version=\""2.3.20\"" />
    <PackageReference Include = \""Newtonsoft.Json\"" Version=\""12.0.2\"" />
    <PackageReference Include = \""RestSharp\"" Version=\""105.1.0\"" />
    <PackageReference Include = \""System.ComponentModel.Annotations\"" Version=\""4.5.0\"" />
    <PackageReference Include = \""System.Runtime.Serialization.Primitives\"" Version=\""4.3.0\"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
@"<Project Sdk=\""Microsoft.NET.Sdk\"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = \""JsonSubTypes\"" Version=\""1.2.0\"" />
    <PackageReference Include = \""Microsoft.Rest.ClientRuntime\"" Version=\""2.3.20\"" />
    <PackageReference Include = \""Newtonsoft.Json\"" Version=\""12.0.2\"" />
    <PackageReference Include = \""RestSharp\"" Version=\""105.1.0\"" />
    <PackageReference Include = \""System.ComponentModel.Annotations\"" Version=\""4.5.0\"" />
    <PackageReference Include = \""System.Runtime.Serialization.Primitives\"" Version=\""4.3.0\"" />
  </ItemGroup>
</Project>";
    }
}
