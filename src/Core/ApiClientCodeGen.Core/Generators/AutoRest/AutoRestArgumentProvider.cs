using System.IO;
using Rapicgen.Core.Options.AutoRest;

namespace Rapicgen.Core.Generators.AutoRest
{
    public interface IAutoRestArgumentProvider
    {
        string GetArguments(string outputFolder, string swaggerFile, string defaultNamespace);
        string GetLegacyArguments(string outputFile, string swaggerFile, string defaultNamespace);
    }

    public class AutoRestArgumentProvider : IAutoRestArgumentProvider
    {
        private readonly IAutoRestOptions options;

        public AutoRestArgumentProvider(IAutoRestOptions options)
        {
            this.options = options;
        }

        public string GetArguments(
            string outputFolder,
            string swaggerFile,
            string defaultNamespace)
        {
            return AppendCommonArguments(
                swaggerFile,
                "--use:@autorest/csharp@3.0.0-beta.20210504.2 " +
                $"--input-file=\"{swaggerFile}\" " +
                $"--output-folder=\"{outputFolder}\" " +
                $"--namespace=\"{defaultNamespace}\" ");
        }

        public string GetLegacyArguments(
            string outputFile,
            string swaggerFile,
            string defaultNamespace)
        {
            return AppendCommonArguments(
                swaggerFile,
                "--csharp " +
                $"--input-file=\"{swaggerFile}\" " +
                $"--output-file=\"{outputFile}\" " +
                $"--namespace=\"{defaultNamespace}\" ");
        }

        private string AppendCommonArguments(
            string swaggerFile,
            string arguments)
        {
            arguments += "--verbose --debug ";

            if (options.AddCredentials)
                arguments += "--add-credentials ";

            arguments += $"--client-side-validation=\"{options.ClientSideValidation}\" ";
            arguments += $"--sync-methods=\"{options.SyncMethods}\" ";

            if (options.UseDateTimeOffset)
                arguments += "--use-datetimeoffset ";

            if (options.UseInternalConstructors)
                arguments += " --use-internal-constructors ";

            if (!options.OverrideClientName)
                return arguments;

            var file = new FileInfo(swaggerFile);
            var name = file.Name
                .Replace(" ", string.Empty)
                .Replace(file.Extension, string.Empty);

            arguments += $" --override-client-name=\"{name}\"";
            return arguments;
        }
    }
}
