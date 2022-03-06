using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ProjectExtensions
    {
        public static object GetSelectedItem()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            object selectedObject = null;

            var monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            try
            {
                monitorSelection.GetCurrentSelection(
                    out var hierarchyPointer,
                    out var itemId,
                    out var multiItemSelect,
                    out var selectionContainerPointer);


                if (Marshal.GetTypedObjectForIUnknown(
                    hierarchyPointer,
                    typeof(IVsHierarchy)) is IVsHierarchy selectedHierarchy)
                    ErrorHandler.ThrowOnFailure(
                        selectedHierarchy.GetProperty(
                            itemId,
                            (int)__VSHPROPID.VSHPROPID_ExtObject,
                            out selectedObject));

                Marshal.Release(hierarchyPointer);
                Marshal.Release(selectionContainerPointer);
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Trace.TraceError(ex.ToString());
            }

            return selectedObject;
        }

        public static async Task InstallMissingPackagesAsync(
            this Community.VisualStudio.Toolkit.Project project,
            SupportedCodeGenerator codeGenerator)
        {
            var requiredPackages = codeGenerator.GetDependencies();
            foreach (var packageDependency in requiredPackages)
            {
                var packageId = packageDependency.Name;
                var version = packageDependency.Version;

                var process = new ProcessLauncher();
                await Task.Run(
                    () => process.Start(
                        "dotnet",
                        $@"add ""{project.FullPath}"" package ""{packageId}"" --version {version}"));
            }
        }

        //public static async Task InstallMissingPackagesAsync(
        //    this Project project,
        //    AsyncPackage package,
        //    SupportedCodeGenerator codeGenerator)
        //{
        //    var options = VsPackage.Instance.GetDialogPage(typeof(GeneralOptionPage)) as IGeneralOptions;
        //    if (options?.InstallMissingPackages == false)
        //    {
        //        Trace.WriteLine("Skipping automatic depedency package installation");
        //        return;
        //    }

        //    Trace.WriteLine("Checking required dependencies");

        //    var componentModel = (IComponentModel)await package.GetServiceAsync(typeof(SComponentModel));
        //    Assumes.Present(componentModel);

        //    var packageInstaller = componentModel.GetService<IVsPackageInstaller>();
        //    var installedServices = componentModel.GetService<IVsPackageInstallerServices>();
        //    var installedPackages = installedServices.GetInstalledPackages(project)?.ToList() ?? new List<IVsPackageMetadata>();

        //    var requiredPackages = codeGenerator.GetDependencies();
        //    foreach (var packageDependency in requiredPackages)
        //    {
        //        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        //        InstallPackageDependency(project, packageDependency, installedPackages, packageInstaller);
        //    }
        //}

        //private static void InstallPackageDependency(
        //    Project project,
        //    PackageDependency packageDependency,
        //    IReadOnlyCollection<IVsPackageMetadata> installedPackages,
        //    IVsPackageInstaller packageInstaller)
        //{
        //    var packageId = packageDependency.Name;
        //    var version = packageDependency.Version;

        //    if (installedPackages.Any(c => string.Equals(c.Id, packageId, StringComparison.InvariantCultureIgnoreCase)) &&
        //       (installedPackages.Any(c => c.VersionString == version) || !packageDependency.ForceUpdate))
        //    {
        //        Trace.WriteLine($"{packageDependency.Name} is already installed");
        //        return;
        //    }

        //    ThreadHelper.ThrowIfNotOnUIThread();
        //    if (packageDependency.IsSystemLibrary)
        //    {
        //        Trace.WriteLine("Package is a system library");
        //        if (!project.IsNetStandardLibrary())
        //        {
        //            Trace.WriteLine("Skipping package installation");
        //            return;
        //        }
        //    }

        //    Trace.WriteLine($"Installing {packageId} version {version}");

        //    try
        //    {
        //        packageInstaller.InstallPackage(
        //                null,
        //                project,
        //                packageId,
        //                version,
        //                true);

        //        Trace.WriteLine($"Successfully installed {packageId} version {version}");
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.TrackError(e);
        //        Trace.WriteLine($"Unable to install {packageId} version {version}");
        //        Trace.WriteLine(e);
        //    }
        //}

        //public static string GetTopLevelNamespace(this Project item)
        //{
        //    try
        //    {
        //        ThreadHelper.ThrowIfNotOnUIThread();

        //        var model = item.CodeModel;
        //        foreach (CodeElement element in model.CodeElements)
        //        {
        //            if (element.Kind == vsCMElement.vsCMElementNamespace)
        //            {
        //                return element.FullName;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.TrackError(e);
        //        Trace.WriteLine("Unable to read top level namespace from Project");
        //        Trace.WriteLine(e);
        //    }
        //    return null;
        //}

        public static string GetTopLevelNamespace(this Community.VisualStudio.Toolkit.Project item)
        {
            try
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(item.Children.FirstOrDefault().FullPath);
                CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
                MemberDeclarationSyntax firstMember = root.Members[0];
                var namespaceDeclaration = (NamespaceDeclarationSyntax)firstMember;
                return namespaceDeclaration.ToFullString();
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.WriteLine("Unable to read top level namespace from Project");
                Trace.WriteLine(e);
            }
            return null;
        }

        //public static bool IsNetStandardLibrary(this Project project)
        //{
        //    try
        //    {
        //        ThreadHelper.ThrowIfNotOnUIThread();

        //        var fileName = project.FileName;
        //        Trace.WriteLine("Project filename = " + fileName);
        //        var contents = File.ReadAllText(fileName);
        //        var result = contents.Contains("<TargetFramework>netstandard");
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Instance.TrackError(e);
        //        Trace.WriteLine("Unable to read project file contents");
        //        Trace.WriteLine(e);
        //    }

        //    return false;
        //}

        //public static async Task UpdatePropertyGroupsAsync(
        //    this Project project,
        //    IReadOnlyDictionary<string, string> properties)
        //{
        //    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        //    project.Save();
        //    var projectFileUpdater = new ProjectFileUpdater(project.FileName);
        //    projectFileUpdater.UpdatePropertyGroup(properties);
        //}
    }

    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";
        public const string NODE_JS = "{9092AA53-FB77-4645-B42D-1CCCA6BD08BD}";
        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";
    }
}