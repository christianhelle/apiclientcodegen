using System;
using System.Diagnostics.CodeAnalysis;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag
{
    public class NSwagCSharpCodeGenerator : ICodeGenerator
    {
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly INSwagCodeGeneratorSettingsFactory generatorSettingsFactory;
        private readonly string swaggerFile;

        public NSwagCSharpCodeGenerator(
            string swaggerFile,
            IOpenApiDocumentFactory documentFactory,
            INSwagCodeGeneratorSettingsFactory generatorSettingsFactory)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.generatorSettingsFactory = generatorSettingsFactory ??
                                            throw new ArgumentNullException(nameof(generatorSettingsFactory));
        }

        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = "This is code is called from an old pre-TPL interface")]
        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            try
            {
                pGenerateProgress?.Progress(10);
                var document = documentFactory.GetDocumentAsync(swaggerFile).GetAwaiter().GetResult();
                pGenerateProgress?.Progress(20);
                var settings = generatorSettingsFactory.GetGeneratorSettings(document);
                pGenerateProgress?.Progress(50);
                var generator = new CSharpClientGenerator(document, settings);
                return generator.GenerateFile();
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }
    }
}