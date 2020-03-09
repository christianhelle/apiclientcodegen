using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace ApiClientCodeGen.VSMac.CommandHandlers
{
    public class NSwagCliCommandHandler : CommandHandler
    {
        protected override void Run()
        {
        }

        protected override void Run(object dataItem)
        {
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workbench.ActiveDocument?.Editor != null;
        }
    }
}
