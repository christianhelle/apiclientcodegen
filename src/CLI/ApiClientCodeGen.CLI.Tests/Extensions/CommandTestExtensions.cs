using System;
using System.Reflection;
using System.Threading;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Tests.Extensions
{
    internal static class CommandTestExtensions
    {
        public static int InvokeExecute<T>(
            this Command<T> command,
            CommandContext? context,
            T settings,
            CancellationToken cancellationToken)
            where T : CommandSettings
        {
            var type = command.GetType();
            while (type != null)
            {
                var method = type.GetMethod(
                    "Execute",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (method != null)
                {
                    return (int)method.Invoke(command, new object?[] { context, settings, cancellationToken })!;
                }

                type = type.BaseType;
            }

            throw new InvalidOperationException($"Execute method not found on {command.GetType().Name}");
        }
    }
}
