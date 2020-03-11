using System;
using System.IO;
using ApiClientCodeGen.VSMac.Commands.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        private string nswagStudioFile;
        private readonly GenerateNSwagStudioCommand command;

        public GenerateNSwagStudioCommandHandler()
        {
            command = Container.Instance.Resolve<GenerateNSwagStudioCommand>();
        }

        protected override void Run() => command.Run(nswagStudioFile);

        protected override void Run(object dataItem) => command.Run(nswagStudioFile);

        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(".nswag", StringComparison.OrdinalIgnoreCase) == true;
            nswagStudioFile = item?.FilePath;
        }
    }
}