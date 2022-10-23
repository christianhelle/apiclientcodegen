using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.Generators.OpenApi
{
    public class OpenApiTypeScriptCodeGenerator : OpenApiCodeGenerator
    {
        private readonly OpenApiTypeScriptGenerator typeScriptGenerator;

        public OpenApiTypeScriptCodeGenerator(
            string swaggerFile,
            string outputPath,
            OpenApiTypeScriptGenerator typeScriptGenerator,
            IGeneralOptions options,
            IProcessLauncher processLauncher,
            IDependencyInstaller dependencyInstaller)
            : base(swaggerFile, outputPath, null!, options, processLauncher, dependencyInstaller)
        {
            this.typeScriptGenerator = typeScriptGenerator;
        }

        protected override string GetGeneratorName()
            => typeScriptGenerator switch
            {
                OpenApiTypeScriptGenerator.ReduxQuery => "typescript-redux-query",
                _ => "typescript-" + typeScriptGenerator.ToString().ToLower()
            };
    }
}