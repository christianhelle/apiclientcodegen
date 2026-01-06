using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateSwaggerCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateSwaggerBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%SwaggerCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

[VisualStudioContribution]
public class GenerateSwaggerNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateSwaggerBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%SwaggerCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

public abstract class GenerateSwaggerBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.WriteLine($"Starting Swagger Codegen code generation for: {inputFile}");
        
        if (inputFile == null)
        {
            Logger.Instance.WriteLine("No input file specified");
            return;
        }

        try
        {
            Logger.Instance.WriteLine("Loading Swagger Codegen configuration...");
            var generalOptions = await settingsProvider.GetGeneralOptionsAsync(cancellationToken);
            
            Logger.Instance.WriteLine("Initializing Swagger Codegen code generator...");
            var generator = new SwaggerCSharpCodeGenerator(
                inputFile,
                defaultNamespace,
                options: generalOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            Logger.Instance.WriteLine("Generating C# client code...");
            var csharpCode = await Task.Run(() => generator.GenerateCode(null));
            if (csharpCode is not null)
            {
                var outputFile = OutputFile.GetOutputFilename(inputFile);
                Logger.Instance.WriteLine($"Writing generated code to: {outputFile}");
                await File.WriteAllTextAsync(
                    outputFile,
                    csharpCode,
                    cancellationToken);
                Logger.Instance.WriteLine("Swagger Codegen code generation completed successfully");
            }
        }
        catch (Exception e)
        {
            Logger.Instance.WriteLine($"Swagger Codegen code generation failed: {e.Message}");
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Swagger Codegen client code: {0}",
                e.Message);

            Logger.Instance.WriteLine("Error generating Swagger Codegen client code: " + e.Message);
        }
    }
}
