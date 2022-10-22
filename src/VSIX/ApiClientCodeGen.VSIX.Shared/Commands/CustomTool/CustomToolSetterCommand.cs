using System.Diagnostics.CodeAnalysis;

namespace Rapicgen.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public static class CustomToolSetterCommand
    {
        public const string ContextGuid = "A3381E62-5D85-436F-824E-5F0097387C11";
        public const string Name = "UI Context";
        public const string Expression = "json | yaml";

        public const string TermNameJson = "json";
        public const string TermNameYaml = "yaml";

        public const string TermValueJson = "HierSingleSelectionName:.json$";
        public const string TermValueYaml = "HierSingleSelectionName:.yaml";
    }
}