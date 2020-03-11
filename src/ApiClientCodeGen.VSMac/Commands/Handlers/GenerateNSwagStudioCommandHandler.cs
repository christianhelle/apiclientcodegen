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
        private readonly GenerateNSwagStudioCommand command;

        public GenerateNSwagStudioCommandHandler()
        {
            command = Container.Instance.Resolve<GenerateNSwagStudioCommand>();
        }

        protected override string SupportedFileExtension => ".nswag";

        protected override void Run() => command.Run(FilePath);

        protected override void Run(object dataItem) => command.Run(FilePath);
    }
}