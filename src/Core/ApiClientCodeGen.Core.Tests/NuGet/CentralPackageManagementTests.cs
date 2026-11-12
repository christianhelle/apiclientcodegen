using System;
using System.IO;
using FluentAssertions;
using Rapicgen.Core.NuGet;
using Xunit;

namespace ApiClientCodeGen.Core.Tests.NuGet
{
    public class CentralPackageManagementTests : IDisposable
    {
        private readonly string root;
        private readonly string projectFile;

        public CentralPackageManagementTests()
        {
            root = Path.Combine(Path.GetTempPath(), "rapicgen-cpm-" + Guid.NewGuid().ToString("N"));
            var projectDir = Path.Combine(root, "src", "MyProject");
            Directory.CreateDirectory(projectDir);
            projectFile = Path.Combine(projectDir, "MyProject.csproj");
            File.WriteAllText(projectFile, "<Project Sdk=\"Microsoft.NET.Sdk\" />");
        }

        public void Dispose()
        {
            try { Directory.Delete(root, true); } catch { }
        }

        private void WritePackagesProps(string directory, string contents)
            => File.WriteAllText(Path.Combine(directory, "Directory.Packages.props"), contents);

        [Fact]
        public void FindCentralPackageVersion_Returns_Null_Without_Props_File()
            => CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();

        [Fact]
        public void FindCentralPackageVersion_Returns_Version_From_Ancestor_Props_File()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Is_Case_Insensitive()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Supports_Msbuild_Namespace()
        {
            WritePackagesProps(
                root,
                "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\"><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Returns_Null_When_Package_Not_Listed()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Uses_Nearest_Props_File()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"1.0.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Theory]
        [InlineData("9.0.2", null, true, "9.0.2")]
        [InlineData("9.0.2", "16.4.0", true, "16.4.0")]
        [InlineData("9.0.2", "16.4.0", false, "16.4.0")]
        [InlineData("9.0.2", "9.0.2", true, "9.0.2")]
        [InlineData("9.0.2", "8.0.0", true, "9.0.2")]
        [InlineData("9.0.2", "8.0.0", false, "8.0.0")]
        public void GetVersionToInstall_Never_Downgrades_Central_Version(
            string requiredVersion,
            string? centralVersion,
            bool forceUpdate,
            string? expected)
            => CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", requiredVersion, forceUpdate),
                    centralVersion)
                .Should()
                .Be(expected);

        [Fact]
        public void FindCentralPackageVersion_Reads_Version_Metadata_Element()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\"><Version>16.4.0</Version></PackageVersion></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Prefers_Update_Over_Include()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"11.2.0\" /><PackageVersion Update=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Follows_Import_To_Parent_Props_File()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><Import Project=\"$([MSBuild]::GetPathOfFileAbove(Directory.Packages.props, $(MSBuildThisFileDirectory)..))\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Does_Not_Search_Parent_Without_Import()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Theory]
        [InlineData("<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" Condition=\"'$(TargetFramework)' == 'net8.0'\" /></ItemGroup></Project>")]
        [InlineData("<Project><ItemGroup Condition=\"'$(TargetFramework)' == 'net8.0'\"><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>")]
        [InlineData("<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /><PackageVersion Include=\"Refit\" Version=\"11.2.0\" /></ItemGroup></Project>")]
        [InlineData("<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"$(RefitVersion)\" /></ItemGroup></Project>")]
        [InlineData("<Project><ItemGroup><PackageVersion Include=\"Refit\"><Version Condition=\"'$(TargetFramework)' == 'net8.0'\">16.4.0</Version><Version Condition=\"'$(TargetFramework)' == 'netstandard2.0'\">11.2.0</Version></PackageVersion></ItemGroup></Project>")]
        [InlineData("<Project><ItemGroup><PackageVersion Include=\"$(RefitPackageId)\" Version=\"16.4.0\" /></ItemGroup></Project>")]
        public void Unresolvable_Central_Version_Skips_Installation(string contents)
        {
            WritePackagesProps(root, contents);

            var centralVersion = CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit");

            centralVersion.Should().NotBeNull();
            CentralPackageManagement
                .GetVersionToInstall(new PackageDependency("Refit", "11.2.0"), centralVersion)
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Returns_Null_When_Project_Opts_Out()
        {
            File.WriteAllText(
                projectFile,
                "<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Ignores_Update_Before_Include()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Update=\"Refit\" Version=\"11.2.0\" /><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Uses_Last_Opt_Out_Assignment()
        {
            File.WriteAllText(
                projectFile,
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally><ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Theory]
        [InlineData("<Project><PropertyGroup Condition=\"'$(Configuration)' == 'Debug'\"><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>")]
        [InlineData("<Project><PropertyGroup><ManagePackageVersionsCentrally Condition=\"'$(Configuration)' == 'Debug'\">false</ManagePackageVersionsCentrally></PropertyGroup></Project>")]
        [InlineData("<Project><PropertyGroup><ManagePackageVersionsCentrally>$(UseCpm)</ManagePackageVersionsCentrally></PropertyGroup></Project>")]
        public void Conditional_Opt_Out_Skips_Installation(string projectContents)
        {
            File.WriteAllText(projectFile, projectContents);
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Follows_Relative_Import()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "Versions.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"$(MSBuildThisFileDirectory)Versions.props\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Does_Not_Search_Parent_For_Unrelated_Import()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"1.0.0\" /></ItemGroup></Project>");
            File.WriteAllText(
                Path.Combine(projectDir, "Versions.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"Versions.props\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void Unresolvable_Import_Skips_Installation()
        {
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><Import Project=\"$(RepoRoot)eng/Versions.props\" /></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Ignores_Update_Before_Imported_Include()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><ItemGroup><PackageVersion Update=\"Refit\" Version=\"11.2.0\" /></ItemGroup><Import Project=\"../../Directory.Packages.props\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Applies_Update_After_Imported_Include()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"11.2.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><Import Project=\"../../Directory.Packages.props\" /><ItemGroup><PackageVersion Update=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Matches_Package_In_Include_List()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Polly; Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Keeps_Version_After_Metadata_Only_Update()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /><PackageVersion Update=\"Refit\" PrivateAssets=\"all\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Returns_Null_When_Directory_Build_Props_Opts_Out()
        {
            File.WriteAllText(
                Path.Combine(root, "Directory.Build.props"),
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void Project_Overrides_Directory_Build_Props_Opt_Out()
        {
            File.WriteAllText(
                Path.Combine(root, "Directory.Build.props"),
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            File.WriteAllText(
                projectFile,
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void Conditional_Import_Skips_Installation()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "Versions.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"Versions.props\" Condition=\"'$(Configuration)' == 'Debug'\" /></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Follows_Import_With_Exists_Condition()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "Versions.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"Versions.props\" Condition=\"Exists('Versions.props')\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Follows_Imports_In_Directory_Build_Props()
        {
            File.WriteAllText(
                Path.Combine(root, "Cpm.props"),
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            File.WriteAllText(
                Path.Combine(root, "Directory.Build.props"),
                "<Project><Import Project=\"$(MSBuildThisFileDirectory)Cpm.props\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void Unresolvable_Import_In_Directory_Build_Props_Skips_Installation()
        {
            File.WriteAllText(
                Path.Combine(root, "Directory.Build.props"),
                "<Project><Import Project=\"$(RepoRoot)Cpm.props\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Skips_Import_When_Exists_Condition_Is_False()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "Versions.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"Versions.props\" Condition=\"Exists('flag.txt')\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void Conditional_Import_After_Local_Declaration_Skips_Installation()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "Legacy.props"),
                "<Project><ItemGroup><PackageVersion Update=\"Refit\" Version=\"11.2.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup><Import Project=\"Legacy.props\" Condition=\"'$(TargetFramework)' == 'netstandard2.0'\" /></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetVersionToInstall_Rejects_Expression_In_Prerelease_Suffix(bool forceUpdate)
            => CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0", forceUpdate),
                    "16.4.0-$(Suffix)")
                .Should()
                .BeNull();

        [Fact]
        public void FindCentralPackageVersion_Honors_GetPathOfFileAbove_Start_Directory()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            var srcDir = Path.GetDirectoryName(projectDir)!;
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                srcDir,
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"$([MSBuild]::GetPathOfFileAbove(Directory.Packages.props, $(MSBuildThisFileDirectory)../..))\" /></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void Unresolvable_GetPathOfFileAbove_Start_Directory_Skips_Installation()
        {
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"11.2.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                Path.GetDirectoryName(projectFile)!,
                "<Project><Import Project=\"$([MSBuild]::GetPathOfFileAbove(Directory.Packages.props, $(RepoRoot)))\" /></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void Exists_Condition_With_Suffixed_Expression_Skips_Installation()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Polly\" Version=\"8.0.0\" /></ItemGroup></Project>");
            File.WriteAllText(
                Path.Combine(projectDir, "Legacy.props"),
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"11.2.0\" /></ItemGroup></Project>");
            WritePackagesProps(
                projectDir,
                "<Project><Import Project=\"Legacy.props\" Condition=\"Exists('$([MSBuild]::GetPathOfFileAbove(Directory.Packages.props, $(MSBuildThisFileDirectory)..)).missing')\" /></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Follows_Project_Imports_For_Opt_Out()
        {
            var projectDir = Path.GetDirectoryName(projectFile)!;
            File.WriteAllText(
                Path.Combine(projectDir, "NoCpm.props"),
                "<Project><PropertyGroup><ManagePackageVersionsCentrally>false</ManagePackageVersionsCentrally></PropertyGroup></Project>");
            File.WriteAllText(
                projectFile,
                "<Project Sdk=\"Microsoft.NET.Sdk\"><Import Project=\"NoCpm.props\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Ignores_Msbuild_Toolset_Imports_In_Project()
        {
            File.WriteAllText(
                projectFile,
                "<Project><Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" /><Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void Custom_Sdk_Import_In_Project_Skips_Installation()
        {
            File.WriteAllText(
                projectFile,
                "<Project><Import Project=\"Sdk.props\" Sdk=\"Contoso.Build.Sdk\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Fact]
        public void FindCentralPackageVersion_Ignores_Microsoft_Sdk_Imports_In_Project()
        {
            File.WriteAllText(
                projectFile,
                "<Project><Import Project=\"Sdk.props\" Sdk=\"Microsoft.NET.Sdk\" /><Import Project=\"Sdk.targets\" Sdk=\"Microsoft.NET.Sdk\" /></Project>");
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Theory]
        [InlineData("<Project Sdk=\"Contoso.Build.Sdk\" />")]
        [InlineData("<Project Sdk=\"Microsoft.NET.Sdk;Contoso.Build.Sdk/1.0.0\" />")]
        [InlineData("<Project><Sdk Name=\"Contoso.Build.Sdk\" Version=\"1.0.0\" /></Project>")]
        public void Implicit_Custom_Sdk_Skips_Installation(string projectContents)
        {
            File.WriteAllText(projectFile, projectContents);
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", "11.2.0"),
                    CentralPackageManagement.FindCentralPackageVersion(projectFile, "Refit"))
                .Should()
                .BeNull();
        }

        [Theory]
        [InlineData("<Project Sdk=\"Microsoft.NET.Sdk\" />")]
        [InlineData("<Project Sdk=\"Microsoft.NET.Sdk.Web/9.0.0\" />")]
        [InlineData("<Project><Sdk Name=\"Microsoft.NET.Sdk\" /></Project>")]
        public void FindCentralPackageVersion_Ignores_Implicit_Microsoft_Sdk(string projectContents)
        {
            File.WriteAllText(projectFile, projectContents);
            WritePackagesProps(
                root,
                "<Project><ItemGroup><PackageVersion Include=\"Refit\" Version=\"16.4.0\" /></ItemGroup></Project>");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .Be("16.4.0");
        }

        [Fact]
        public void FindCentralPackageVersion_Returns_Null_For_Invalid_Xml()
        {
            WritePackagesProps(root, "not xml");

            CentralPackageManagement
                .FindCentralPackageVersion(projectFile, "Refit")
                .Should()
                .BeNull();
        }
    }
}
