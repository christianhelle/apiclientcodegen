using System;
using System.ComponentModel.Design;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public interface ICustomToolSetter
    {
        Task InitializeAsync(AsyncPackage package);
    }

    public abstract class CustomToolSetter : ICustomToolSetter
    {
        public const string ContextGuid = "A3381E62-5D85-436F-824E-5F0097387C11";
        public const string Name = "UI Context";
        public const string Expression = "json";
        public const string TermValue = "HierSingleSelectionName:.json$";

        public abstract Task InitializeAsync(AsyncPackage package);
    }

    public abstract class CustomToolSetter<T>
        : ICustomToolSetter
        where T : IVsSingleFileGenerator
    {
        private DTE dte;

        protected abstract int CommandId { get; }
        protected abstract Guid CommandSet { get; }

        public async Task InitializeAsync(AsyncPackage package)
        {
            dte = (DTE)await package.GetServiceAsync(typeof(DTE));
            var commandService = (IMenuCommandService)await package.GetServiceAsync((typeof(IMenuCommandService)));
            var cmdId = new CommandID(CommandSet, CommandId);
            var cmd = new MenuCommand(OnExecute, cmdId);
            commandService.AddCommand(cmd);
        }

        private void OnExecute(object sender, EventArgs e)
        {
            var item = dte.SelectedItems.Item(1).ProjectItem;
            item.Properties.Item("CustomTool").Value = typeof(T).Name;
        }
    }

    public class AutoRestCodeGeneratorCustomToolSetter
        : CustomToolSetter<AutoRestCodeGenerator>
    {
        public const string Name = nameof(AutoRestCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0100;

        protected override Guid CommandSet { get; }
            = new Guid("C5A13119-924D-4A05-A530-33C1D55B3729");
    }

    public class NSwagCodeGeneratorCustomToolSetter
        : CustomToolSetter<NSwagCodeGenerator>
    {
        public const string Name = nameof(NSwagCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0200;

        protected override Guid CommandSet { get; }
            = new Guid("765CF48A-ABD5-42C5-8D58-59D1872A90A9");
    }

    public class SwaggerCodeGeneratorCustomToolSetter
        : CustomToolSetter<SwaggerCodeGenerator>
    {
        public const string Name = nameof(SwaggerCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0300;

        protected override Guid CommandSet { get; }
            = new Guid("C14BC613-573D-4AAA-B922-B38B57CD8A47");
    }
}