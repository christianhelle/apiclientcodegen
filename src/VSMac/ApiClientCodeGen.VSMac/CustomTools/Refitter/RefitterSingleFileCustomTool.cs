using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Refitter;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Refitter;

namespace ApiClientCodeGen.VSMac.CustomTools.Refitter
{
    public class RefitterSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "RefitterCodeGenerator";

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => new RefitterCodeGenerator(
                swaggerFile, 
                customToolNamespace, 
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                new DefaultRefitterOptions());
    }
}