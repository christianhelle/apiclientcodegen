# AutoRest Migration Guide

⚠️ **IMPORTANT:** AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.

## Migration Recommendations

This guide helps you migrate from AutoRest to modern, actively maintained code generators. Choose the option that best fits your needs:

### Option 1: NSwag (Recommended for most users)

**Best for:** Traditional HttpClient-based REST clients with full control over HTTP communication.

**Benefits:**
- Battle-tested and widely used in production
- Comprehensive feature set with extensive configuration options
- Active development and community support
- Similar output style to AutoRest
- Built-in dependency injection support

**How to migrate:**
1. In Visual Studio, change the custom tool from `AutoRestCodeGenerator` to `NSwagCodeGenerator`
2. In CLI, use `rapicgen csharp nswag` instead of `rapicgen csharp autorest`
3. Review and update any AutoRest-specific configuration options

**Example CLI:**
```bash
# Before (AutoRest)
rapicgen csharp autorest swagger.json GeneratedCode Output.cs

# After (NSwag)
rapicgen csharp nswag swagger.json GeneratedCode Output.cs
```

### Option 2: Refitter (Recommended for Refit users)

**Best for:** Projects using or wanting to use [Refit](https://github.com/reactiveui/refit) for type-safe REST clients.

**Benefits:**
- Generates Refit interfaces instead of full implementations
- Cleaner, more maintainable interface-based code
- Excellent for testing and mocking
- Built on top of NSwag for contract generation
- Actively maintained with frequent updates

**How to migrate:**
1. Add the [Refit](https://www.nuget.org/packages/Refit/) NuGet package to your project
2. In Visual Studio, change the custom tool to `RefitterCodeGenerator`
3. In CLI, use `rapicgen csharp refitter`
4. Update client instantiation code to use `RestService.For<IApiClient>(httpClient)`

**Example CLI:**
```bash
# Before (AutoRest)
rapicgen csharp autorest swagger.json GeneratedCode Output.cs

# After (Refitter)
rapicgen csharp refitter swagger.json GeneratedCode Output.cs
```

### Option 3: Microsoft Kiota (Recommended for Microsoft Graph APIs)

**Best for:** Microsoft Graph API and other Microsoft services, or projects wanting modern async-first patterns.

**Benefits:**
- Official Microsoft replacement for AutoRest
- Modern, async-first design
- Excellent for Microsoft Graph and Azure APIs
- Built-in middleware pipeline support
- Active Microsoft support

**How to migrate:**
1. Add required Kiota NuGet packages (see [Dependencies](#kiota-dependencies) below)
2. In Visual Studio, change the custom tool to `KiotaCodeGenerator`
3. In CLI, use `rapicgen csharp kiota`
4. Update client instantiation to use Kiota's builder pattern

**Example CLI:**
```bash
# Before (AutoRest)
rapicgen csharp autorest swagger.json GeneratedCode Output.cs

# After (Kiota)
rapicgen csharp kiota swagger.json GeneratedCode Output.cs
```

## Feature Comparison

| Feature | AutoRest | NSwag | Refitter | Kiota |
|---------|----------|-------|----------|-------|
| Status | ⚠️ Deprecated | ✅ Active | ✅ Active | ✅ Active |
| OpenAPI 3.x Support | Partial | ✅ Full | ✅ Full | ✅ Full |
| Async/Await | ✅ | ✅ | ✅ | ✅ |
| Dependency Injection | ✅ | ✅ | ✅ | ✅ |
| Single File Output | ✅ | ✅ | ✅ | ✅ |
| Contract Generation | ✅ | ✅ | ✅ | ✅ |
| Interface-based | ❌ | ❌ | ✅ | ❌ |
| Middleware Pipeline | Limited | ✅ | Via HttpClient | ✅ Built-in |
| Testing Support | Good | Good | Excellent | Good |

## Dependencies

### NSwag Dependencies
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) 13.0.3+

### Refitter Dependencies
- [Refit](https://www.nuget.org/packages/Refit/) 9.0.2+

### Kiota Dependencies
- [Microsoft.Kiota.Abstractions](https://www.nuget.org/packages/Microsoft.Kiota.Abstractions)
- [Microsoft.Kiota.Http.HttpClientLibrary](https://www.nuget.org/packages/Microsoft.Kiota.Http.HttpClientLibrary)
- [Microsoft.Kiota.Serialization.Form](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Form)
- [Microsoft.Kiota.Serialization.Json](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Json)
- [Microsoft.Kiota.Serialization.Text](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Text)
- [Microsoft.Kiota.Serialization.Multipart](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Multipart)

## Migration Timeline

- **Now through June 2026:** AutoRest remains functional with deprecation warnings
- **July 1, 2026:** Microsoft retires AutoRest
- **Future major version (estimated Q3-Q4 2026):** AutoRest support removed from this tool

## Need Help?

If you encounter issues during migration:

1. Review the generator-specific documentation:
   - [NSwag Documentation](https://github.com/RicoSuter/NSwag/wiki)
   - [Refitter Documentation](https://github.com/christianhelle/refitter)
   - [Kiota Documentation](https://learn.microsoft.com/en-us/openapi/kiota/)

2. Check the [project issues](https://github.com/christianhelle/apiclientcodegen/issues) for known migration challenges

3. Create a new issue if you need assistance with your specific use case
