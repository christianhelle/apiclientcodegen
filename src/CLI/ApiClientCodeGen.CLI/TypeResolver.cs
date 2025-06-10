using System;
using Spectre.Console.Cli;

namespace Rapicgen.CLI;

public sealed class TypeResolver(IServiceProvider provider) : ITypeResolver
{
    public object? Resolve(Type? type)
    {
        return type != null ? provider.GetService(type) : null;
    }
}