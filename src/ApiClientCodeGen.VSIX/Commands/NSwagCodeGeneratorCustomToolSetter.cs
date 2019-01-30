using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public class NSwagCodeGeneratorCustomToolSetter
        : CustomToolSetter<NSwagCodeGenerator>
    {
        public const string Name = nameof(NSwagCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0200;

        protected override Guid CommandSet { get; }
            = new Guid("765CF48A-ABD5-42C5-8D58-59D1872A90A9");
    }
}