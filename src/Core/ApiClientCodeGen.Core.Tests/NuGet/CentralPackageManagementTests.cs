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
            string expected)
            => CentralPackageManagement
                .GetVersionToInstall(
                    new PackageDependency("Refit", requiredVersion, forceUpdate),
                    centralVersion)
                .Should()
                .Be(expected);

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
