namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class KiotaProjectFileContents
    {
        public const string NetCoreApp =
            """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net8.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.Kiota.Abstractions" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Http.HttpClientLibrary" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Form" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Json" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Multipart" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Text" Version="2.0.0" />
              </ItemGroup>
            </Project>
            """;

        public const string NetStandardLibrary =
            """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>netstandard2.1</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.Kiota.Abstractions" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Http.HttpClientLibrary" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Form" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Json" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Multipart" Version="2.0.0" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Text" Version="2.0.0" />
              </ItemGroup>
            </Project>
            """;
    }
}
