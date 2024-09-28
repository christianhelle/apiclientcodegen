namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class NSwagProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""5.0.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Newtonsoft.Json"" Version=""13.0.1"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""5.0.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";
    }
}
