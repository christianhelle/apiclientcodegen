using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Logging;
using Refitter.Core;
using System.Diagnostics;
using System.Text.Json;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateRefitterCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateRefitterBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration
        => new("%RefitterCommand.DisplayName%")
        {
            Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
            VisibleWhen = ActivationConstraint.ClientContext(
            ClientContextKey.Shell.ActiveSelectionFileName,
            ".(json|ya?ml)")
        };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateCodeAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

[VisualStudioContribution]
public class GenerateRefitterSettingsCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateRefitterBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration
        => new("%RefitterSettingsCommand.DisplayName%")
        {
            Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
            Placements = [KnownPlacements.Node_IncludeExcludeGroup],
            VisibleWhen = ActivationConstraint.ClientContext(
                ClientContextKey.Shell.ActiveSelectionFileName,
                ".(refitter)")
        };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateCodeAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

[VisualStudioContribution]
public class GenerateRefitterNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateRefitterBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration
        => new("%RefitterCommand.DisplayName%")
        {
            Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken)
    {
        await GenerateCodeAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
}

[VisualStudioContribution]
public abstract class GenerateRefitterBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;
    private readonly JsonSerializerOptions serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task GenerateCodeAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.WriteLine($"Starting Refitter code generation for: {inputFile}");
        
        if (inputFile == null)
        {
            Logger.Instance.WriteLine("No input file specified");
            return;
        }

        Logger.Instance.TrackFeatureUsage("Generate Refitter output");

        try
        {
            var csharpCode = await GenerateCodeInternalAsync(inputFile, defaultNamespace, cancellationToken);
            if (csharpCode is not null)
            {
                var outputFile = OutputFile.GetOutputFilename(inputFile);
                Logger.Instance.WriteLine($"Writing generated code to: {outputFile}");
                await File.WriteAllTextAsync(
                    outputFile,
                    csharpCode,
                    cancellationToken);
                Logger.Instance.WriteLine("Refitter code generation completed successfully");
            }
        }
        catch (Exception e)
        {
            Logger.Instance.WriteLine($"Refitter code generation failed: {e.Message}");
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Refit client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating Refit client code: " + e.Message,
                cancellationToken);
        }
    }

    public async Task<string> GenerateCodeInternalAsync(string inputFile, string defaultNamespace, CancellationToken cancellationToken)
    {
        RefitGeneratorSettings settings;
        if (inputFile.EndsWith(".refitter"))
        {
            Logger.Instance.WriteLine("Loading Refitter settings file...");
            settings = Serializer
                .Deserialize<RefitGeneratorSettings>(
                    File.ReadAllText(inputFile));
        }
        else
        {
            var fileInfo = new FileInfo(inputFile);
            var refitterFile = fileInfo.Name.Replace(fileInfo.Extension, ".refitter");
            if (File.Exists(refitterFile))
            {
                Logger.Instance.WriteLine($"Loading Refitter settings from: {refitterFile}");
                settings = Serializer.Deserialize<RefitGeneratorSettings>(
                    File.ReadAllText(refitterFile)
                );
            }
            else
            {
                Logger.Instance.WriteLine("Loading Refitter configuration...");
                var options = await settingsProvider.GetRefitterOptionsAsync(cancellationToken);
                settings = new RefitGeneratorSettings
                {
                    OpenApiPath = inputFile,
                    Namespace = defaultNamespace,
                    AddAutoGeneratedHeader = options.AddAutoGeneratedHeader,
                    GenerateContracts = options.GenerateContracts,
                    GenerateXmlDocCodeComments = options.GenerateXmlDocCodeComments,
                    ReturnIApiResponse = options.ReturnIApiResponse,
                    UseCancellationTokens = options.UseCancellationTokens,
                    GenerateOperationHeaders = options.GenerateHeaderParameters,
                    GenerateMultipleFiles = options.GenerateMultipleFiles
                };

                var settingsFile = Path.ChangeExtension(fileInfo.FullName, ".refitter");
                Logger.Instance.WriteLine($"Saving Refitter settings to: {settingsFile}");
                File.WriteAllText(
                    settingsFile,
                    JsonSerializer.Serialize(settings, serializerOptions));
            }
        }

        Directory.SetCurrentDirectory(Path.GetDirectoryName(inputFile)!);
        
        Logger.Instance.WriteLine("Initializing Refitter code generator...");
        var generator = await RefitGenerator.CreateAsync(settings);

        using var context = new DependencyContext(
            "Refitter",
            JsonSerializer.Serialize(settings, serializerOptions));

        if (settings.GenerateMultipleFiles)
        {
            Logger.Instance.WriteLine("Generating multiple files...");
            var fileInfo = new FileInfo(inputFile);
            var outputFolder = fileInfo.Directory!.FullName;
            if (settings.OutputFolder != RefitGeneratorSettings.DefaultOutputFolder)
            {
                outputFolder = Path.Combine(outputFolder, settings.OutputFolder);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
            }

            var results = generator.GenerateMultipleFiles();
            Logger.Instance.WriteLine($"Writing {results.Files.Count()} files to: {outputFolder}");
            foreach (var file in results.Files)
            {
                File.WriteAllText(Path.Combine(outputFolder, file.Filename), file.Content);
            }

            context.Succeeded();

            return string.Empty;
        }
        else
        {
            Logger.Instance.WriteLine("Generating C# client code...");
            var code = generator.Generate();
            context.Succeeded();

            var output = Rapicgen.Core.Generators.GeneratedCode.PrefixAutogeneratedCodeHeader(code, "Refitter", "v1.7.1");

            if (inputFile.EndsWith(".refitter"))
            {
                var fileInfo = new FileInfo(inputFile);
                var outputFolder = fileInfo.Directory!.FullName;
                if (settings.OutputFolder != RefitGeneratorSettings.DefaultOutputFolder)
                {
                    outputFolder = Path.Combine(outputFolder, settings.OutputFolder);
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                }

                var targetFilename = Path.ChangeExtension(fileInfo.Name, ".cs");
                var targetPath = Path.Combine(outputFolder, targetFilename);
                Logger.Instance.WriteLine($"Writing generated code to: {targetPath}");
                File.WriteAllText(targetPath, output);
            }

            return output;
        }
    }
}
