using System;
using System.Linq;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class GenerateCommandHandler : BaseCommandHandler
    {
        protected abstract string GeneratorName { get; }
        protected virtual string SupportedFileExtension => ".json;.yaml;.yml";
        protected FilePath FilePath { get; private set; }

        protected override void Run() => SetGenerator();
        protected override void Run(object dataItem) => SetGenerator();

        protected override void Update(CommandInfo info)
        {
            if (!(IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFile projectFile))
            {
                info.Visible = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(SupportedFileExtension))
                throw new InvalidOperationException(
                    $"{nameof(SupportedFileExtension)} must not be null or whitespace");

            info.Visible = IsSupported(projectFile);
            FilePath = projectFile.FilePath;
        }

        private void SetGenerator()
        {
            var project = IdeApp.ProjectOperations.CurrentSelectedProject;
            var item = project.Files.GetFile(FilePath);
            var projectFile = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            item.Generator = IsSupported(projectFile) ? GeneratorName : null;
            IdeApp.ProjectOperations.MarkFileDirty(item.FilePath);
        }

        private bool IsSupported(ProjectFile projectFile)
        {
            var extensions = SupportedFileExtension.Split(';');
            return extensions?.Length > 1
                ? extensions.Any(ext => IsSupported1(projectFile, ext))
                : IsSupported1(projectFile, SupportedFileExtension);
        }

        private static bool IsSupported1(ProjectFile projectFile, string extension)
        {
            return projectFile?.Name?.EndsWith(
                extension,
                StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}