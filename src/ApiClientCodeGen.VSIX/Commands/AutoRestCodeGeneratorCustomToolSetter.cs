using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.AutoRest;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public class AutoRestCodeGeneratorCustomToolSetter
        : CustomToolSetter<AutoRestCodeGenerator>
    {
        public const string Name = nameof(AutoRestCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0100;

        protected override Guid CommandSet { get; }
            = new Guid("C5A13119-924D-4A05-A530-33C1D55B3729");
    }
}