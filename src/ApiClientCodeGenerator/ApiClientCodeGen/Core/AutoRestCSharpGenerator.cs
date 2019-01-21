using System.Diagnostics;
using System.IO;

namespace ApiClientCodeGen
{
    public class AutoRestCSharpGenerator : ICodeGenerator
    {
        private readonly string swaggerFile;
        private readonly string defaultNamespace;

        public AutoRestCSharpGenerator(string swaggerFile, string defaultNamespace)
        {
            this.swaggerFile = swaggerFile ?? throw new System.ArgumentNullException(nameof(swaggerFile));
            this.defaultNamespace = defaultNamespace ?? throw new System.ArgumentNullException(nameof(defaultNamespace));
        }

        public string GenerateCode()
        {
            var outputFile = Path.Combine(
                Path.GetDirectoryName(swaggerFile),
                "TempApiClient.cs");

            var process = Process.Start($"autorest " +
                $"--csharp " +
                $"--input-file='{swaggerFile}' " +
                $"--output-file='{outputFile}' " +
                $"--namespace='{defaultNamespace}' " +
                $"--add-credentials");

            try
            {
                return File.ReadAllText(outputFile);
            }
            finally
            {
                File.Delete(outputFile);
            }
        }
    }
}
