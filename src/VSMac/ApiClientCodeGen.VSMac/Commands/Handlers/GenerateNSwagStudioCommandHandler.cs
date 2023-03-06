using System;
using ApiClientCodeGen.VSMac.Commands.NSwagStudio;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        private readonly GenerateNSwagStudioCommand command;

        public GenerateNSwagStudioCommandHandler()
        {
            var processLauncher = new ProcessLauncher();
            command = new GenerateNSwagStudioCommand(
                new NSwagStudioCodeGeneratorFactory(
                    new DefaultGeneralOptions(),
                    processLauncher,
                    new DependencyInstaller(
                        new NpmInstaller(processLauncher),
                        new FileDownloader(new WebDownloader()),
                        processLauncher)));
        }

        protected override string GeneratorName
            => throw new NotSupportedException();
        
        protected override string SupportedFileExtension => ".nswag";

        protected override void Run() => command.Run(FilePath);

        protected override void Run(object dataItem) => command.Run(FilePath);
    }
}