using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool.Swagger;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands
{
    public class SwaggerCodeGeneratorCustomToolSetter
        : CustomToolSetter<SwaggerCodeGenerator>
    {
        public const string Name = nameof(SwaggerCodeGeneratorCustomToolSetter);

        protected override int CommandId { get; } = 0x0300;

        protected override Guid CommandSet { get; }
            = new Guid("C14BC613-573D-4AAA-B922-B38B57CD8A47");
    }
}