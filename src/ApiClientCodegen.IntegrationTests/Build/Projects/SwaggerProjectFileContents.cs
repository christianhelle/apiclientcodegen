namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build.Projects
{
    public class SwaggerProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""JsonSubTypes"" Version=""1.2.0"" />
    <PackageReference Include = ""RestSharp"" Version=""105.1.0"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""JsonSubTypes"" Version=""1.2.0"" />
    <PackageReference Include = ""RestSharp"" Version=""105.1.0"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";
    }
}