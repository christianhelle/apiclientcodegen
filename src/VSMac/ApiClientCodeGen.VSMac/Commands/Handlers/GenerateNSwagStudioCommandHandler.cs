using System;
using ApiClientCodeGen.VSMac.Commands.NSwagStudio;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        private readonly GenerateNSwagStudioCommand command;

        public GenerateNSwagStudioCommandHandler()
        {
            command = Container.Instance.Resolve<GenerateNSwagStudioCommand>();
        }

        protected override string GeneratorName
            => throw new NotSupportedException();
        
        protected override string SupportedFileExtension => ".nswag";

        protected override void Run() => command.Run(FilePath);

        protected override void Run(object dataItem) => command.Run(FilePath);
    }
}