using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "ApiClientCodeGen.VSMac",
    Namespace = "ApiClientCodeGen.VSMac",
    Version = "1.0"
)]

[assembly: AddinName("ApiClientCodeGen")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("A collection of code generators for Swagger / OpenAPI specification files")]
[assembly: AddinAuthor("Christian Resma Helle")]
