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

        public const string NetFrameworkApp =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""15.0"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>{17CC408B-0A2E-4D5E-BC30-51FB5131A593}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>ClassLibrary</RootNamespace>
    <AssemblyName>ClassLibrary</AssemblyName>
    <TargetFrameworkVersion>v4.5.1</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <Deterministic>true</Deterministic>
    <RuntimeIdentifiers>win</RuntimeIdentifiers>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
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
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""Generated.cs"" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include=""JsonSubTypes"">
      <Version>1.6.0</Version>
    </PackageReference>
    <PackageReference Include=""Newtonsoft.Json"">
      <Version>12.0.2</Version>
    </PackageReference>
    <PackageReference Include=""RestSharp"">
      <Version>105.2.3</Version>
    </PackageReference>
  </ItemGroup>
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
</Project>";
    }
}