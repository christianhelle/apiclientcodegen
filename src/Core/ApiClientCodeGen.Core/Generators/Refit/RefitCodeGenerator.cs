using Refitter.Core;

namespace Rapicgen.Core.Generators.Refit;

public class RefitCodeGenerator : ICodeGenerator
{
    private readonly string swaggerFile;
    private readonly string defaultNamespace;

    public RefitCodeGenerator(
        string swaggerFile,
        string defaultNamespace)
    {
        this.swaggerFile = swaggerFile;
        this.defaultNamespace = defaultNamespace;
    }
    
    public string GenerateCode(IProgressReporter? pGenerateProgress)
    {
        pGenerateProgress?.Progress(10);
        var generator = RefitGenerator.Create(
            new RefitGeneratorSettings
            {
                OpenApiPath = swaggerFile,
                Namespace = defaultNamespace
            })
            .GetAwaiter()
            .GetResult();
        
        pGenerateProgress?.Progress(50);
        var code = generator.Generate();
        
        pGenerateProgress?.Progress(90);
        return code;
    }
}