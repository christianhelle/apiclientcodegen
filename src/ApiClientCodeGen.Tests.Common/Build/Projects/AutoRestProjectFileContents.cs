namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public class AutoRestProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Rest.ClientRuntime"" Version=""2.3.21"" />
    <PackageReference Include = ""Newtonsoft.Json"" Version=""12.0.3"" />
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
    <PackageReference Include = ""Microsoft.Rest.ClientRuntime"" Version=""2.3.21"" />
    <PackageReference Include = ""Newtonsoft.Json"" Version=""12.0.3"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";
    }
}