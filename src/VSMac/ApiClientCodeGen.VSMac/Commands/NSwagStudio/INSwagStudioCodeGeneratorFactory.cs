using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ApiClientCodeGen.VSMac.Commands.NSwagStudio
{
    public interface INSwagStudioCodeGeneratorFactory
    {
        NSwagStudioCodeGenerator Create(string nswagStudioFile); 
    }

    public class NSwagStudioCodeGeneratorFactory : INSwagStudioCodeGeneratorFactory
    {
        private readonly IGeneralOptions generalOptions;
        private readonly IProcessLauncher processLauncher;

        public NSwagStudioCodeGeneratorFactory(
            IGeneralOptions generalOptions,
            IProcessLauncher processLauncher)
        {
            this.generalOptions = generalOptions ?? throw new ArgumentNullException(nameof(generalOptions));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }
        
        public NSwagStudioCodeGenerator Create(string nswagStudioFile)
            => new NSwagStudioCodeGenerator(
                nswagStudioFile,
                generalOptions,
                processLauncher);
    }
}