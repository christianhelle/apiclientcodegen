using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using FluentAssertions;
using Rapicgen.Commands.AddNew;
using Rapicgen.Commands.CustomTool;
using Xunit;

namespace Rapicgen.Tests.Commands
{
    public class CommandTableTests
    {
        private static readonly XNamespace Ns = "http://schemas.microsoft.com/VisualStudio/2005-10-18/CommandTable";

        private static readonly string[] CommandTables =
        {
            Path.Combine("src", "VSIX", "ApiClientCodeGen.VSIX", "VSCommandTable.vsct"),
            Path.Combine("src", "VSIX", "ApiClientCodeGen.VSIX.Dev17", "VSCommandTable.vsct"),
        };

        public static IEnumerable<object[]> Commands()
        {
            var types = typeof(CustomToolSetterCommand).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract)
                .ToList();

            foreach (var vsct in CommandTables)
            {
                foreach (var type in types.Where(IsCustomToolSetter))
                    yield return new object[] { vsct, type.Name, type.Name };

                foreach (var type in types.Where(t => typeof(NewRestClientCommand).IsAssignableFrom(t)))
                {
                    yield return new object[] { vsct, type.Name, "GenerateWith" + GetGeneratorName(type) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(Commands))]
        public void CommandId_Matches_CommandTable(string vsct, string typeName, string symbol)
        {
            var type = typeof(CustomToolSetterCommand).Assembly
                .GetTypes()
                .Single(t => t.Name == typeName);
            var instance = Activator.CreateInstance(type);
            var commandId = (int)GetProperty(type, "CommandId").GetValue(instance);
            var commandSet = (Guid)GetProperty(type, "CommandSet").GetValue(instance);

            var guidSymbol = XDocument.Load(FindFile(vsct))
                .Descendants(Ns + "GuidSymbol")
                .Single(g => Guid.Parse((string)g.Attribute("value")) == commandSet);
            var idSymbol = guidSymbol
                .Elements(Ns + "IDSymbol")
                .SingleOrDefault(e => (string)e.Attribute("name") == symbol);

            idSymbol.Should().NotBeNull($"{symbol} should exist in {vsct}");
            Convert.ToInt32((string)idSymbol!.Attribute("value"), 16)
                .Should()
                .Be(commandId, $"{typeName} must match {symbol} in {vsct}");
        }

        private static bool IsCustomToolSetter(Type type)
        {
            for (var t = type.BaseType; t != null; t = t.BaseType)
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(CustomToolSetter<>))
                    return true;
            return false;
        }

        private static string GetGeneratorName(Type type)
        {
            const string prefix = "New";
            const string suffix = "ClientCommand";
            var name = type.Name;
            if (name.StartsWith(prefix, StringComparison.Ordinal))
                name = name.Substring(prefix.Length);
            if (name.EndsWith(suffix, StringComparison.Ordinal))
                name = name.Substring(0, name.Length - suffix.Length);
            return name;
        }

        private static PropertyInfo GetProperty(Type type, string name)
            => type.GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)!;

        private static string FindFile(string relativePath)
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var path = Path.Combine(dir.FullName, relativePath);
                if (File.Exists(path))
                    return path;
                dir = dir.Parent;
            }

            throw new FileNotFoundException(relativePath);
        }
    }
}
