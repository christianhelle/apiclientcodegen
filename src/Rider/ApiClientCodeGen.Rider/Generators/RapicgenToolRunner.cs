using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Application.Environment;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.Util;

namespace Rapicgen.Rider.Generators
{
    public class RapicgenToolRunner
    {
        private readonly ILogger _logger;
        private const int ExecutionTimeout = 120000; // 2 minutes
        
        public RapicgenToolRunner(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsToolInstalled()
        {
            try
            {
                var result = ExecuteCommand("dotnet", "tool list -g", true);
                return result.Contains("rapicgen");
            }
            catch (Exception ex)
            {
                _logger.Error("Error checking for Rapicgen tool installation", ex);
                return false;
            }
        }

        public bool InstallTool()
        {
            try
            {
                var result = ExecuteCommand("dotnet", "tool install --global rapicgen", true);
                return result.Contains("successfully installed") || result.Contains("Tool 'rapicgen' is already installed");
            }
            catch (Exception ex)
            {
                _logger.Error("Error installing Rapicgen tool", ex);
                return false;
            }
        }

        public async Task<string> GenerateCode(
            string generator, 
            string openApiSpecificationPath, 
            string defaultNamespace, 
            string outputPath)
        {
            if (string.IsNullOrEmpty(generator))
                throw new ArgumentException("Generator name is required", nameof(generator));
            
            if (string.IsNullOrEmpty(openApiSpecificationPath))
                throw new ArgumentException("OpenAPI specification path is required", nameof(openApiSpecificationPath));

            if (!File.Exists(openApiSpecificationPath))
                throw new FileNotFoundException("OpenAPI specification file not found", openApiSpecificationPath);

            defaultNamespace ??= "GeneratedCode";
            
            // Ensure output directory exists
            var outputDirectory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Build the command arguments
            var arguments = $"csharp {generator} \"{openApiSpecificationPath}\" \"{defaultNamespace}\" \"{outputPath}\"";

            _logger.Info($"Running rapicgen with arguments: {arguments}");
            
            // Execute the rapicgen tool
            try
            {
                var output = await Task.Run(() => ExecuteCommand("rapicgen", arguments));
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error generating code with rapicgen: {ex.Message}", ex);
                throw new Exception($"Failed to generate code: {ex.Message}", ex);
            }
        }
        
        public async Task<string> GenerateTypeScriptCode(
            string generator, 
            string openApiSpecificationPath, 
            string outputDirectory)
        {
            if (string.IsNullOrEmpty(generator))
                throw new ArgumentException("Generator name is required", nameof(generator));
            
            if (string.IsNullOrEmpty(openApiSpecificationPath))
                throw new ArgumentException("OpenAPI specification path is required", nameof(openApiSpecificationPath));

            if (!File.Exists(openApiSpecificationPath))
                throw new FileNotFoundException("OpenAPI specification file not found", openApiSpecificationPath);
            
            // Ensure output directory exists
            if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Build the command arguments
            var arguments = $"typescript {generator} \"{openApiSpecificationPath}\" \"{outputDirectory}\"";

            _logger.Info($"Running rapicgen with arguments: {arguments}");
            
            // Execute the rapicgen tool
            try
            {
                var output = await Task.Run(() => ExecuteCommand("rapicgen", arguments));
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error generating TypeScript code with rapicgen: {ex.Message}", ex);
                throw new Exception($"Failed to generate TypeScript code: {ex.Message}", ex);
            }
        }

        private string ExecuteCommand(string command, string arguments, bool redirectErrorStream = false)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();
                
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        outputBuilder.AppendLine(args.Data);
                    }
                };
                
                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        errorBuilder.AppendLine(args.Data);
                    }
                };
                
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                
                if (!process.WaitForExit(ExecutionTimeout))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                        // Ignore errors from killing the process
                    }
                    
                    throw new TimeoutException($"Command execution timed out after {ExecutionTimeout / 1000} seconds");
                }
                
                var output = outputBuilder.ToString();
                var error = errorBuilder.ToString();
                
                if (process.ExitCode != 0 && !redirectErrorStream)
                {
                    throw new Exception($"Command execution failed with exit code {process.ExitCode}. Error: {error}");
                }
                
                return redirectErrorStream ? output + error : output;
            }
        }
    }
}
