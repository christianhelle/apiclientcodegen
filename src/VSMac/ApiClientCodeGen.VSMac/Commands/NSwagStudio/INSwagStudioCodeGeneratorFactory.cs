using System;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

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
        private readonly IDependencyInstaller dependencyInstaller;

        public NSwagStudioCodeGeneratorFactory(
            IGeneralOptions generalOptions,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
        {
            this.generalOptions = generalOptions ?? throw new ArgumentNullException(nameof(generalOptions));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.dependencyInstaller = dependencyInstaller ?? throw new ArgumentNullException(nameof(dependencyInstaller));
        }
        
        public NSwagStudioCodeGenerator Create(string nswagStudioFile)
            => new NSwagStudioCodeGenerator(
                nswagStudioFile,
                generalOptions,
                processLauncher,
                dependencyInstaller);
    }
}