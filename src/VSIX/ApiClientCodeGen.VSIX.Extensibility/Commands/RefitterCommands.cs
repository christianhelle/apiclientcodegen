using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.RpcContracts.ProgressReporting;
using Rapicgen.Core.Logging;
using Refitter.Core;
using System.Diagnostics;
using System.Text.Json;
using ProgressReporter = Microsoft.VisualStudio.Extensibility.Shell.ProgressReporter;

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
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Refitter");
        await GenerateCodeAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
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
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Refitter");
        await GenerateCodeAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
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
        Logger.Instance.TrackFeatureUsage("New REST API Client (Refitter)");
        await GenerateCodeAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
}

[VisualStudioContribution]
public abstract class GenerateRefitterBaseCommand(
    TraceSource traceSource,
    ExtensionSettingsProvider settingsProvider)
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
        using var progress = await Extensibility
            .Shell()
            .StartProgressReportingAsync(
                "Generating code with Refitter",
                new ProgressReporterOptions(true),
                cancellationToken);

        cancellationToken = CancellationTokenSource
            .CreateLinkedTokenSource(
                cancellationToken,
                progress.CancellationToken)
            .Token;

        if (inputFile == null)
        {
            progress.Report(100, "No input file specified");
            return;
        }

        var inputFilename = Path.GetFileName(inputFile);
        progress.Report(10, $"Starting Refitter code generation for: {inputFilename}");

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var csharpCode = await GenerateCodeInternalAsync(
                inputFile,
                defaultNamespace,
                progress,
                cancellationToken);

            if (csharpCode is not null)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var outputFile = OutputFile.GetOutputFilename(inputFile);

                var outputFilename = Path.GetFileName(outputFile);
                progress.Report(90, $"Writing generated code to: {outputFilename}");

                await File.WriteAllTextAsync(
                    outputFile,
                    csharpCode,
                    cancellationToken);

                progress.Report(100, "Refitter code generation completed successfully");
            }
        }
        catch (OperationCanceledException)
        {
            progress.Report(100, "Refitter code generation was canceled.");
        }
        catch (Exception e)
        {
            Logger.Instance.TrackError(e);
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Refit client code: {0}",
                e.Message);

            progress.Report(100, "Error generating Refit client code: " + e.Message);
        }
    }

    public async Task<string> GenerateCodeInternalAsync(
        string inputFile,
        string defaultNamespace,
        ProgressReporter progress,
        CancellationToken cancellationToken)
    {
        RefitGeneratorSettings settings;
        if (inputFile.EndsWith(".refitter"))
        {
            progress.Report(20, "Loading Refitter settings file...");
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
                cancellationToken.ThrowIfCancellationRequested();
                progress.Report(30, $"Loading Refitter settings from: {refitterFile}");
                settings = Serializer.Deserialize<RefitGeneratorSettings>(
                    File.ReadAllText(refitterFile)
                );
            }
            else
            {
                cancellationToken.ThrowIfCancellationRequested();
                progress.Report(30, "Loading Refitter configuration...");
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

                cancellationToken.ThrowIfCancellationRequested();
                var settingsFile = Path.ChangeExtension(fileInfo.FullName, ".refitter");
                progress.Report(40, $"Saving Refitter settings to: {settingsFile}");
                File.WriteAllText(
                    settingsFile,
                    JsonSerializer.Serialize(settings, serializerOptions));
            }
        }

        cancellationToken.ThrowIfCancellationRequested();
        Directory.SetCurrentDirectory(Path.GetDirectoryName(inputFile)!);

        progress.Report(50, "Initializing Refitter code generator...");
        var generator = await RefitGenerator.CreateAsync(settings);

        using var context = new DependencyContext(
            "Refitter",
            JsonSerializer.Serialize(settings, serializerOptions));

        if (settings.GenerateMultipleFiles)
        {
            progress.Report(60, "Generating multiple files...");
            var fileInfo = new FileInfo(inputFile);
            var outputFolder = fileInfo.Directory!.FullName;
            if (settings.OutputFolder != RefitGeneratorSettings.DefaultOutputFolder)
            {
                cancellationToken.ThrowIfCancellationRequested();
                outputFolder = Path.Combine(outputFolder, settings.OutputFolder);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
            }

            cancellationToken.ThrowIfCancellationRequested();
            var results = generator.GenerateMultipleFiles();
            progress.Report(80, $"Writing {results.Files.Count} files to: {outputFolder}");

            foreach (var file in results.Files)
            {
                cancellationToken.ThrowIfCancellationRequested();
                File.WriteAllText(Path.Combine(outputFolder, file.Filename), file.Content);
            }

            context.Succeeded();

            return string.Empty;
        }
        else
        {
            progress.Report(60, "Generating C# client code...");

            cancellationToken.ThrowIfCancellationRequested();
            var code = generator.Generate();
            context.Succeeded();

            var output = Rapicgen.Core.Generators.GeneratedCode.PrefixAutogeneratedCodeHeader(code, "Refitter", "v1.7.1");

            if (inputFile.EndsWith(".refitter"))
            {
                var fileInfo = new FileInfo(inputFile);
                var outputFolder = fileInfo.Directory!.FullName;
                if (settings.OutputFolder != RefitGeneratorSettings.DefaultOutputFolder)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    outputFolder = Path.Combine(outputFolder, settings.OutputFolder);
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();
                var targetFilename = Path.ChangeExtension(fileInfo.Name, ".cs");
                var targetPath = Path.Combine(outputFolder, targetFilename);
                progress.Report(80, $"Writing generated code to: {targetPath}");
                File.WriteAllText(targetPath, output);
            }

            return output;
        }
    }
}