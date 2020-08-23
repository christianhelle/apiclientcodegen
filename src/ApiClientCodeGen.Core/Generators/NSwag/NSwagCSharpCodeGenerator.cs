using System;
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

        public string GenerateCode(IProgressReporter pGenerateProgress)
        {
            try
            {
                pGenerateProgress?.Progress(10);
                var document = documentFactory.GetDocument(swaggerFile);
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