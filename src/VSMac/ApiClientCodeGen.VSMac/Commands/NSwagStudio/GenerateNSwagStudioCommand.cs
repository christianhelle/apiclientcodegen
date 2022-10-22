using System;
using Rapicgen.Core.Logging;

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

            Logger.Instance.TrackFeatureUsage("Generate NSwag Studio output", "VSMac");

            var codeGenerator = factory.Create(file);
            codeGenerator.GenerateCode(null);
        }
    }
}