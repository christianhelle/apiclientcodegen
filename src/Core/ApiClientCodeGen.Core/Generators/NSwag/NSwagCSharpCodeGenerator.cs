﻿using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using NSwag.CodeGeneration.CSharp;

namespace Rapicgen.Core.Generators.NSwag
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
            this.swaggerFile = swaggerFile;
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.generatorSettingsFactory = generatorSettingsFactory ??
                                            throw new ArgumentNullException(nameof(generatorSettingsFactory));
        }

        public string GenerateCode(IProgressReporter? pGenerateProgress)
        {
            try
            {
                using var context = new DependencyContext("NSwag");
                var code = OnGenerateCode(pGenerateProgress);
                context.Succeeded();
                return code;
            }
            finally
            {
                pGenerateProgress?.Progress(90);
            }
        }

        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = "This is code is called from an old pre-TPL interface")]
        private string OnGenerateCode(IProgressReporter? pGenerateProgress)
        {
            pGenerateProgress?.Progress(10);
            var document = documentFactory.GetDocumentAsync(swaggerFile).GetAwaiter().GetResult();
            pGenerateProgress?.Progress(20);
            var settings = generatorSettingsFactory.GetGeneratorSettings(document);
            pGenerateProgress?.Progress(50);
            var generator = new CSharpClientGenerator(document, settings);
            return generator.GenerateFile();
        }
    }
}