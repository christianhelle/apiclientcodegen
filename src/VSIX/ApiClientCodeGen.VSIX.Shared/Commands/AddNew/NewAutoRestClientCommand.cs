using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NSwag;
using Task = System.Threading.Tasks.Task;

namespace Rapicgen.Commands.AddNew
{
    [ExcludeFromCodeCoverage]
    public class NewAutoRestClientCommand : NewRestClientCommand
    {
        protected override int CommandId { get; } = 0x0200;

        protected override SupportedCodeGenerator CodeGenerator { get; } = SupportedCodeGenerator.AutoRest;

        protected override async Task OnInstallPackagesAsync(
            AsyncPackage package,
            Project project,
            EnterOpenApiSpecDialogResult dialogResult)
        {
            var url = dialogResult.Url;
            const StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
            var document = url.EndsWith("yaml", comparisonType) || url.EndsWith("yml", comparisonType)
                ? await OpenApiYamlDocument.FromYamlAsync(dialogResult.OpenApiSpecification)
                : await OpenApiDocument.FromJsonAsync(dialogResult.OpenApiSpecification);

            var codeGenerator = GetSupportedCodeGenerator(document.OpenApi);
            await project.InstallMissingPackagesAsync(package, codeGenerator);

            if (codeGenerator == SupportedCodeGenerator.AutoRestV3)
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                project.Save();

                await project.UpdatePropertyGroupsAsync(AutoRestConstants.PropertyGroups);
            }
        }

        private static SupportedCodeGenerator GetSupportedCodeGenerator(string openApiSpecVersion)
            => !string.IsNullOrEmpty(openApiSpecVersion) &&
               Version.TryParse(openApiSpecVersion, out var openApiVersion) &&
               openApiVersion > Version.Parse("3.0.0")
                ? SupportedCodeGenerator.AutoRestV3
                : SupportedCodeGenerator.AutoRest;
    }
}