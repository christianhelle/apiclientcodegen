using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Rapicgen.Core;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Logging;
using Rapicgen.Core.NuGet;
using Rapicgen.Core.Options.General;
using Rapicgen.Options.General;
using EnvDTE;
using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using NuGet.VisualStudio;
using Task = System.Threading.Tasks.Task;

namespace Rapicgen.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ProjectExtensions
    {
        public static string? GetRootFolder(this Project project, DTE Dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (project.IsKind("{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")) //ProjectKinds.vsProjectKindSolutionFolder
                return Path.GetDirectoryName(Dte.Solution.FullName);

            if (string.IsNullOrEmpty(project.FullName))
                return null;

            string? fullPath;

            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (ArgumentException)
            {
                try
                {
                    // MFC projects don't have FullPath, and there seems to be no way to query existence
                    fullPath = project.Properties.Item("ProjectDirectory").Value as string;
                }
                catch (ArgumentException)
                {
                    // Installer projects have a ProjectPath.
                    fullPath = project.Properties.Item("ProjectPath").Value as string;
                }
            }

            if (string.IsNullOrEmpty(fullPath))
                return File.Exists(project.FullName) ? Path.GetDirectoryName(project.FullName) : null;

            if (Directory.Exists(fullPath))
                return fullPath;

            if (File.Exists(fullPath))
                return Path.GetDirectoryName(fullPath);

            return null;
        }

        public static ProjectItem? AddFileToProject(this Project project, DTE Dte, FileInfo file, string? itemType = null)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (project.IsKind(ProjectTypes.ASPNET_5, ProjectTypes.SSDT))
                return Dte.Solution.FindProjectItem(file.FullName);

            var root = project.GetRootFolder(Dte);

            if (string.IsNullOrEmpty(root) || !file.FullName.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                return null;

            var item = project.ProjectItems.AddFromFile(file.FullName);
            
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                if (item.ContainingProject == null)
                    return item;

                if (string.IsNullOrEmpty(itemType) ||
                    item.ContainingProject.IsKind(ProjectTypes.WEBSITE_PROJECT) ||
                    item.ContainingProject.IsKind(ProjectTypes.UNIVERSAL_APP))
                    return item;

                item.Properties.Item("ItemType").Value = itemType;
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Logger.Instance.WriteLine(ex);
            }

            return item;
        }

        private static bool IsKind(this Project project, params string[] kindGuids) 
            => kindGuids.Any(guid =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                return project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase);
            });

        public static Project? GetActiveProject(this DTE Dte)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                if (Dte.ActiveSolutionProjects is Array { Length: > 0 } activeSolutionProjects)
                    return activeSolutionProjects.GetValue(0) as Project;

                var doc = Dte.ActiveDocument;

                if (doc != null && !string.IsNullOrEmpty(doc.FullName))
                {
                    var item = Dte.Solution?.FindProjectItem(doc.FullName);

                    if (item != null)
                        return item.ContainingProject;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Logger.Instance.WriteLine("Error getting the active project" + ex);
            }

            return null;
        }

        public static IVsTextView GetCurrentNativeTextView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IVsTextManager textManager = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));
            Assumes.Present(textManager);
            ErrorHandler.ThrowOnFailure(textManager.GetActiveView(1, null, out var activeView));
            return activeView;
        }

        public static object? GetSelectedItem()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            object? selectedObject = null;

            var monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            try
            {
                monitorSelection.GetCurrentSelection(
                    out var hierarchyPointer,
                    out var itemId,
                    out _,
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
            this Project project,
            AsyncPackage package,
            SupportedCodeGenerator codeGenerator)
        {
            var options = VsPackage.Instance.GetDialogPage(typeof(GeneralOptionPage)) as IGeneralOptions;
            if (options?.InstallMissingPackages == false)
            {
                Logger.Instance.WriteLine("Skipping automatic depedency package installation");
                return;
            }

            Logger.Instance.WriteLine("Checking required dependencies");

            var componentModel = (IComponentModel)await package.GetServiceAsync(typeof(SComponentModel));
            Assumes.Present(componentModel);

            var packageInstaller = componentModel.GetService<IVsPackageInstaller>();
#pragma warning disable CS0618
            var installedServices = componentModel.GetService<IVsPackageInstallerServices>();
            var installedPackages = installedServices.GetInstalledPackages(project)?.ToList() ?? new List<IVsPackageMetadata>();
#pragma warning restore CS0618

            OpenApiSupportedVersion openApiGeneratorVersion = (VsPackage.Instance.GetDialogPage(typeof(OpenApiGeneratorOptionsPage)) as IOpenApiGeneratorOptions).Version;
            var requiredPackages = codeGenerator == SupportedCodeGenerator.OpenApi
                ? codeGenerator.GetDependencies(openApiGeneratorVersion)
                : codeGenerator.GetDependencies();
            foreach (var packageDependency in requiredPackages)
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                InstallPackageDependency(project, packageDependency, installedPackages, packageInstaller);
            }
        }

        private static void InstallPackageDependency(
            Project project,
            PackageDependency packageDependency,
            IReadOnlyCollection<IVsPackageMetadata> installedPackages,
            IVsPackageInstaller packageInstaller)
        {
            var packageId = packageDependency.Name;
            var version = packageDependency.Version;

            if (installedPackages.Any(c => string.Equals(c.Id, packageId, StringComparison.InvariantCultureIgnoreCase)) &&
               (installedPackages.Any(c => c.VersionString == version) || !packageDependency.ForceUpdate))
            {
                Logger.Instance.WriteLine($"{packageDependency.Name} is already installed");
                return;
            }

            ThreadHelper.ThrowIfNotOnUIThread();
            if (packageDependency.IsSystemLibrary)
            {
                Logger.Instance.WriteLine("Package is a system library");
                if (!project.IsNetStandardLibrary())
                {
                    Logger.Instance.WriteLine("Skipping package installation");
                    return;
                }
            }

            Logger.Instance.WriteLine($"Installing {packageId} version {version}");

            try
            {
                packageInstaller.InstallPackage(
                        null,
                        project,
                        packageId,
                        version,
                        true);

                Logger.Instance.WriteLine($"Successfully installed {packageId} version {version}");
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Logger.Instance.WriteLine($"Unable to install {packageId} version {version}");
                
            }
        }

        public static string? GetTopLevelNamespace(this Project item)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                var model = item.CodeModel;
                foreach (CodeElement element in model.CodeElements)
                {
                    if (element.Kind == vsCMElement.vsCMElementNamespace)
                    {
                        return element.FullName;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Logger.Instance.WriteLine("Unable to read top level namespace from Project");
                
            }
            return null;
        }

        private static bool IsNetStandardLibrary(this Project project)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                var fileName = project.FileName;
                Logger.Instance.WriteLine("Project filename = " + fileName);
                var contents = File.ReadAllText(fileName);
                var result = contents.Contains("<TargetFramework>netstandard");
                return result;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Logger.Instance.WriteLine("Unable to read project file contents");
                
            }

            return false;
        }

        public static async Task UpdatePropertyGroupsAsync(
            this Project project,
            IReadOnlyDictionary<string, string> properties)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            project.Save();
            var projectFileUpdater = new ProjectFileUpdater(project.FileName);
            projectFileUpdater.UpdatePropertyGroup(properties);
        }
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