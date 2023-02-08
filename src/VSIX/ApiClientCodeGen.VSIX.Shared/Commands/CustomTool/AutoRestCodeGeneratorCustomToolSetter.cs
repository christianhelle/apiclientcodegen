using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.CustomTool.AutoRest;
using Rapicgen.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Rapicgen.Core.Logging;
using Task = System.Threading.Tasks.Task;

namespace Rapicgen.Commands.CustomTool
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

            var project = dte.GetActiveProject()!;

            var type = typeof(AutoRestCodeGenerator);
            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = type.Name;

            var name = type.Name.Replace("CodeGenerator", string.Empty);
            Logger.Instance.WriteLine($"Generating code using {name}");

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

                await project.UpdatePropertyGroupsAsync(
                    AutoRestConstants.PropertyGroups);
            }
            else
            {
                await project.InstallMissingPackagesAsync(
                    package,
                    type.GetSupportedCodeGenerator());
            }
        }
    }
}