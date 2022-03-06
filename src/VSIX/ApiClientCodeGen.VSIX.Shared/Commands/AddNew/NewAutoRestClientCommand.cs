using System;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NSwag;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewAutoRestClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0200;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.AutoRest;

        protected override async Task OnInstallPackagesAsync(
            AsyncPackage package,
            Community.VisualStudio.Toolkit.Project project,
            EnterOpenApiSpecDialogResult dialogResult)
        {
            var url = dialogResult.Url;
            const StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
            var document = url.EndsWith("yaml", comparisonType) || url.EndsWith("yml", comparisonType)
                ? await OpenApiYamlDocument.FromYamlAsync(dialogResult.OpenApiSpecification)
                : await OpenApiDocument.FromJsonAsync(dialogResult.OpenApiSpecification);

            var codeGenerator = GetSupportedCodeGenerator(document);
            await project.InstallMissingPackagesAsync(codeGenerator);

            if (codeGenerator == SupportedCodeGenerator.AutoRestV3)
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                await project.SaveAsync();

                var projectFile = new ProjectFileUpdater(project.FullPath);
                projectFile.UpdatePropertyGroup(AutoRestConstants.PropertyGroups);
            }
        }

        private static SupportedCodeGenerator GetSupportedCodeGenerator(OpenApiDocument document)
            => !string.IsNullOrEmpty(document.OpenApi) &&
               Version.TryParse(document.OpenApi, out var openApiVersion) &&
               openApiVersion > Version.Parse("3.0.0")
                ? SupportedCodeGenerator.AutoRestV3
                : SupportedCodeGenerator.AutoRest;
    }
}