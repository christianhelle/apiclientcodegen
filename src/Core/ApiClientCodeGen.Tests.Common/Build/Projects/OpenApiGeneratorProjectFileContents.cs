﻿namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class OpenApiGeneratorProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Extensions.Http"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Hosting"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Http.Polly"" Version=""5.0.1"" />
    <PackageReference Include = ""System.Threading.Channels"" Version=""8.0.0"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""5.0.0"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <LangVersion>latest</LangVersion>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Extensions.Http"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Hosting"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Http.Polly"" Version=""5.0.1"" />
    <PackageReference Include = ""System.Threading.Channels"" Version=""8.0.0"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""5.0.0"" />
  </ItemGroup>
</Project>";

        public const string NetFrameworkApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net481</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""System"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Runtime.Serialization"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
    <Reference Include=""System.Web"" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include = ""Microsoft.Extensions.Http"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Hosting"" Version=""5.0.0"" />
    <PackageReference Include = ""Microsoft.Extensions.Http.Polly"" Version=""5.0.1"" />
    <PackageReference Include = ""System.Threading.Channels"" Version=""8.0.0"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""5.0.0"" />
  </ItemGroup>
</Project>";
    }
}

