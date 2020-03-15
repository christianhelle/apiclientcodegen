using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class GenerateCommandHandler : BaseCommandHandler
    {
        protected abstract string GeneratorName { get; }
        protected virtual string SupportedFileExtension => ".json";
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

            info.Visible = projectFile.Name.EndsWith(
                SupportedFileExtension,
                StringComparison.OrdinalIgnoreCase);

            FilePath = projectFile.FilePath;
        }

        private void SetGenerator()
        {
            var project = IdeApp.ProjectOperations.CurrentSelectedProject;
            var item = project.Files.GetFile(FilePath);
            item.Generator = GeneratorName;
            IdeApp.ProjectOperations.MarkFileDirty(item.FilePath);
        }
    }
}