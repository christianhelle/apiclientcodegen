using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;


[VisualStudioContribution]
public class AboutCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%AboutCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateRefitterCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%RefitterCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateNSwagCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%NSwagCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateOpenApiCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%OpenApiGeneratorCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateKiotaCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%KiotaCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateSwaggerCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%SwaggerCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

[VisualStudioContribution]
public class GenerateAutoRestCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%AutoRestCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
