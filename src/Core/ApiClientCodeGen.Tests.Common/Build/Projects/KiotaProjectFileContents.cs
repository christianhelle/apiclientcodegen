namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class KiotaProjectFileContents
    {
        public const string NetCoreApp =
            @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""Azure.Identity"" Version=""1.11.2"" />
    <PackageReference Include=""Microsoft.Kiota.Abstractions"" Version=""1.8.4"" />
    <PackageReference Include=""Microsoft.Kiota.Authentication.Azure"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Http.HttpClientLibrary"" Version=""1.4.0"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Form"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Json"" Version=""1.2.3"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Text"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Multipart"" Version=""1.1.5"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
          @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""Azure.Identity"" Version=""1.11.2"" />
    <PackageReference Include=""Microsoft.Kiota.Abstractions"" Version=""1.8.4"" />
    <PackageReference Include=""Microsoft.Kiota.Authentication.Azure"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Http.HttpClientLibrary"" Version=""1.4.0"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Form"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Json"" Version=""1.2.3"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Text"" Version=""1.1.5"" />
    <PackageReference Include=""Microsoft.Kiota.Serialization.Multipart"" Version=""1.1.5"" />
  </ItemGroup>
</Project>";
    }
}