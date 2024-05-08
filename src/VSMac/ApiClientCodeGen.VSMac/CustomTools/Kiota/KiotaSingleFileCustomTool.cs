using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Kiota;

namespace ApiClientCodeGen.VSMac.CustomTools.Kiota
{
    public class KiotaSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "KiotaCodeGenerator";

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => new KiotaCodeGenerator(
                swaggerFile,
                customToolNamespace,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                new DefaultKiotaOptions());
    }
}