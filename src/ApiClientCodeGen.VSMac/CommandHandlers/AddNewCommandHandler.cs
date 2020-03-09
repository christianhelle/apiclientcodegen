using MonoDevelop.Components.Commands;

namespace ApiClientCodeGen.VSMac.CommandHandlers
{
    public class AddNewAutoRestCommandHandler : AddNewCommandHandler
    {
    }

    public class AddNewNSwagCommandHandler : AddNewCommandHandler
    {
    }

    public class AddNewNSwagStudioCommandHandler : AddNewCommandHandler
    {
    }

    public class AddNewSwaggerCommandHandler : AddNewCommandHandler
    {
    }

    public class AddNewOpenApiCommandHandler : AddNewCommandHandler
    {
    }

    public abstract class AddNewCommandHandler : CommandHandler
    {
        protected override void Run()
        {
        }

        protected override void Run(object dataItem)
        {
        }
    }
}
