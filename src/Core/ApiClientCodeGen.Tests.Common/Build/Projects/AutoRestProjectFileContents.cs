namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class AutoRestProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <IncludeGeneratorSharedCode>true</IncludeGeneratorSharedCode>
    <RestoreAdditionalProjectSources>https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json</RestoreAdditionalProjectSources>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Rest.ClientRuntime"" Version=""2.3.21"" />
    <PackageReference Include = ""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
    <PackageReference Include = ""Microsoft.Azure.AutoRest.CSharp"" Version=""3.0.0-beta.20210218.1"" PrivateAssets=""All"" />
    <PackageReference Include = ""Azure.Core"" Version=""1.50.0"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <IncludeGeneratorSharedCode>true</IncludeGeneratorSharedCode>
    <RestoreAdditionalProjectSources>https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json</RestoreAdditionalProjectSources>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Rest.ClientRuntime"" Version=""2.3.21"" />
    <PackageReference Include = ""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
    <PackageReference Include = ""Microsoft.Azure.AutoRest.CSharp"" Version=""3.0.0-beta.20210218.1"" PrivateAssets=""All"" />
    <PackageReference Include = ""Azure.Core"" Version=""1.50.0"" />
  </ItemGroup>
</Project>";
    }
}