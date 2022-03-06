using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using Community.VisualStudio.Toolkit;
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

        protected override async Task OnExecuteAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var type = typeof(AutoRestCodeGenerator);
            var item = await VS.Solutions.GetActiveItemAsync();
            var file = await PhysicalFile.FromFileAsync(item.FullPath);
            await file.TrySetAttributeAsync("CustomTool", type.Name);      

            var name = type.Name.Replace("CodeGenerator", string.Empty);
            Trace.WriteLine($"Generating code using {name}");

            var project = await VS.Solutions.GetActiveProjectAsync();
            var documentFactory = new OpenApiDocumentFactory();
            var swaggerFile = item.FullPath;

            var document = await documentFactory.GetDocumentAsync(swaggerFile);
            if (!string.IsNullOrEmpty(document.OpenApi) &&
                Version.TryParse(document.OpenApi, out var openApiVersion) &&
                openApiVersion > Version.Parse("3.0.0"))
            {
                await project.InstallMissingPackagesAsync(
                    SupportedCodeGenerator.AutoRestV3);

                var projectFile = new ProjectFileUpdater(project.FullPath);
                projectFile.UpdatePropertyGroup(AutoRestConstants.PropertyGroups);
            }
            else
            {
                await project.InstallMissingPackagesAsync(
                    type.GetSupportedCodeGenerator());
            }
        }
    }
}