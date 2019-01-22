using System;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace ApiClientCodeGen.VSIX
{
    [Guid("47AFE4E1-5A52-4FE1-8CA7-EDB8310BDA4A")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("REST API Client Code Generator", "", "1.0")]
    [ProvideCodeGenerator(typeof(AutoRestCSharpCodeGenerator), nameof(AutoRestCodeGenerator), AutoRestCSharpCodeGenerator.Description, true)]
    public sealed class VSPackage : AsyncPackage
    {
    }
}
