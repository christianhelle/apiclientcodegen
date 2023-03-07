using System;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Options.NSwag;

namespace ApiClientCodeGen.VSMac.CustomTools.NSwag
{
    public class NSwagSingleFileCustomTool : BaseSingleFileCustomTool
    {
        public const string GeneratorName = "NSwagCodeGenerator";
        
        private readonly INSwagCodeGeneratorFactory factory;
        private readonly IOpenApiDocumentFactory openApiDocumentFactory;
        private readonly INSwagOptions options;

        public NSwagSingleFileCustomTool()
            : this(
                new NSwagCodeGeneratorFactory(),
                new OpenApiDocumentFactory(),
                new DefaultNSwagOptions())
        {
        }

        public NSwagSingleFileCustomTool(
            INSwagCodeGeneratorFactory factory,
            IOpenApiDocumentFactory openApiDocumentFactory,
            INSwagOptions options)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.openApiDocumentFactory = openApiDocumentFactory ??
                                          throw new ArgumentNullException(nameof(openApiDocumentFactory));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected override ICodeGenerator GetCodeGenerator(
            string swaggerFile,
            string customToolNamespace)
            => factory.Create(
                swaggerFile,
                customToolNamespace,
                options,
                openApiDocumentFactory);
    }
}