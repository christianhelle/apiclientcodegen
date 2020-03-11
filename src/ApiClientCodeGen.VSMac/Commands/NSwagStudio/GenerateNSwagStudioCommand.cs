using System;

namespace ApiClientCodeGen.VSMac.Commands.NSwagStudio
{
    public class GenerateNSwagStudioCommand : ICodeGeneratorCommand
    {
        private readonly INSwagStudioCodeGeneratorFactory factory;

        public GenerateNSwagStudioCommand(INSwagStudioCodeGeneratorFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public void Run(string file, string customNamespace = null)
        {
            if (string.IsNullOrWhiteSpace(file))
                return;

            var codeGenerator = factory.Create(file);
            codeGenerator.GenerateCode(null);
        }
    }
}