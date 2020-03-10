using MonoDevelop.Components.Commands;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class BaseCommandHandler : CommandHandler
    {
        protected BaseCommandHandler()
        {
            LoggingServiceTraceListener.Initialize();
        }
    }
}