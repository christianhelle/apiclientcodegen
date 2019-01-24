using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.Swagger
{
    public class SwaggerCSharpCodeGenerator : CodeGenerator
    {
        public SwaggerCSharpCodeGenerator(string swaggerFile, string defaultNamespace)
            : base(swaggerFile, defaultNamespace)
        {
        }

        protected override string GetArguments(string outputFile)
        {
            throw new NotImplementedException();
        }

        protected override string GetCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
