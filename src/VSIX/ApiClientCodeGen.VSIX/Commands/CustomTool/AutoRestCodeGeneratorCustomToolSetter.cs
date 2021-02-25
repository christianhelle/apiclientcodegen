using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class AutoRestCodeGeneratorCustomToolSetter
        : CustomToolSetter<AutoRestCodeGenerator>
    {
        public const string Name = nameof(AutoRestCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0200;

        protected override async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var project = ProjectExtensions.GetActiveProject(dte);

            var type = typeof(AutoRestCodeGenerator);
            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = type.Name;

            var name = type.Name.Replace("CodeGenerator", string.Empty);
            Trace.WriteLine($"Generating code using {name}");

            var documentFactory = new OpenApiDocumentFactory();
            var swaggerFile = item.FileNames[0];

            var document = await documentFactory.GetDocumentAsync(swaggerFile);
            if (!string.IsNullOrEmpty(document.OpenApi) &&
                Version.TryParse(document.OpenApi, out var openApiVersion) &&
                openApiVersion > Version.Parse("3.0.0"))
            {
                await project.InstallMissingPackagesAsync(
                    package,
                    SupportedCodeGenerator.AutoRestV3);

                project.Save();

                UpdatePropertyGroups(project.FileName);
            }
            else
            {
                await project.InstallMissingPackagesAsync(
                    package,
                    type.GetSupportedCodeGenerator());
            }
        }

        private static void UpdatePropertyGroups(string projectFile)
        {
            var xml = XDocument.Load(projectFile);
            var propertyGroups = xml.Elements("Project").Elements("PropertyGroup").Elements().ToList();

            if (propertyGroups.All(c => c.Name != "IncludeGeneratorSharedCode"))
            {
                propertyGroups.Add(
                    new XElement("IncludeGeneratorSharedCode", true));
            }
            else
            {
                propertyGroups
                    .First(c => c.Name == "IncludeGeneratorSharedCode")
                    .Value = bool.TrueString;
            }

            if (propertyGroups.All(c => c.Name != "RestoreAdditionalProjectSources"))
            {
                propertyGroups.Add(
                    new XElement(
                        "RestoreAdditionalProjectSources",
                        "https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json"));
            }
            else
            {
                propertyGroups
                    .First(c => c.Name == "RestoreAdditionalProjectSources")
                    .Value = "https://azuresdkartifacts.blob.core.windows.net/azure-sdk-tools/index.json";
            }

            xml?.Root?.Element("PropertyGroup")?.ReplaceNodes(propertyGroups);
            xml.Save(projectFile);
        }
    }
}