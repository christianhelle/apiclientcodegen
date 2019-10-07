namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.AutoRest
{
/*
TODO: Add support for generating code using the following options
        
C# Generator (activated by --csharp)

  --azure-arm                   generate code in Azure flavor
  --fluent                      generate code in fluent flavor
  --namespace=<string>          determines the root namespace to be used in generated code
  --license-header=<string>     text to include as a header comment in generated files (magic strings: MICROSOFT_MIT, MICROSOFT_APACHE, MICROSOFT_MIT_NO_VERSION, MICROSOFT_APACHE_NO_VERSION, MICROSOFT_MIT_NO_CODEGEN)
  --payload-flattening-threshold=<number>  max. number of properties in a request body. If the number of properties in the request body is less than or equal to this value, these properties will be represented as individual method arguments instead
  --add-credentials             include a credential property and constructor parameter supporting different authentication behaviors
  --override-client-name=<string>  overrides the name of the client class (usually derived from $.info.title)
  --use-internal-constructors   generate constructors with internal instead of public visibility (useful for convenience layers)
  --sync-methods=<"essential" | "all" | "none">  determines amount of synchronous wrappers to generate; default: essential
  --use-datetimeoffset          use DateTimeOffset instead of DateTime to model date/time types
  --client-side-validation=<boolean>  whether to validate parameters at the client side (according to OpenAPI definition) before making a request; default: true
  --max-comment-columns=<number>  maximum line width of comments before breaking into a new line
  --output-file=<string>        generate all code into the specified, single file (instead of the usual folder structure)
  --sample-generation           generate sample code from x-ms-examples (experimental)
  --static-serializer           generate client serlializer settings as a static member (experimental)
*/

    public interface IAutoRestOptions
    {
    }
}