using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    public abstract class SwaggerCommand
    {
        [Required]
        [FileExists]
        [Argument(0, "swaggerFile", "Path to the Swagger / Open API specification file")]
        public string SwaggerFile { get; set; }

        [Argument(1, "namespace", "Default namespace to in the generated code")]
        public string DefaultNamespace { get; set; }
        
        [Argument(2, "outputFile", "Output filename to write the generated code to. Default is the swaggerFile .cs")]
        public string OutputFile { get; set; }
        
        public virtual Task<int> OnExecuteAsync()
        {
            return Task.FromResult(ResultCodes.Error);
        }
    }
}