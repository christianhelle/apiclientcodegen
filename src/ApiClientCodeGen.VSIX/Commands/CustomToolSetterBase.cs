using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public abstract class CustomToolSetterBase : ICustomToolSetter
    {
        public const string ContextGuid = "A3381E62-5D85-436F-824E-5F0097387C11";
        public const string Name = "UI Context";
        public const string Expression = "json";
        public const string TermValue = "HierSingleSelectionName:.json$";

        public abstract Task InitializeAsync(AsyncPackage package);
    }
}