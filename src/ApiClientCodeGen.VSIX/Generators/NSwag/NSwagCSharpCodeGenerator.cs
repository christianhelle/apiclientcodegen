using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwag;
using Microsoft.VisualStudio.Shell.Interop;
using NSwag.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwag
{
    public class NSwagCSharpCodeGenerator : ICodeGenerator
    {
        private readonly string defaultNamespace;
        private readonly IOpenApiDocumentFactory documentFactory;
        private readonly INSwagCodeGeneratorSettingsFactory generatorSettingsFactory;
        private readonly NSwagCSharpOptions options;
        private readonly string swaggerFile;

        public NSwagCSharpCodeGenerator(
            string swaggerFile,
            string defaultNamespace,
            INSwagOptions options,
            IOpenApiDocumentFactory documentFactory,
            INSwagCodeGeneratorSettingsFactory generatorSettingsFactory)
        {
            this.swaggerFile = swaggerFile ?? throw new ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new ArgumentNullException(nameof(defaultNamespace));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.generatorSettingsFactory = generatorSettingsFactory ??
                                            throw new ArgumentNullException(nameof(generatorSettingsFactory));
            this.options = new NSwagCSharpOptions(options ?? throw new ArgumentNullException(nameof(options)));
        }

        public string GenerateCode(IVsGeneratorProgress pGenerateProgress)
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