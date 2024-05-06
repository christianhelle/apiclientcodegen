using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rapicgen.Core.Extensions;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Generators
{
    public static class CSharpFileMerger
    {
        public static string MergeFiles(string folder)
        {
            var filesToParse = GetSourceFileNames(folder).ToList();

            Logger.Instance.WriteLine($"Found {filesToParse.Count} files to merge");
            foreach (var file in filesToParse)
            {
                Logger.Instance.WriteLine($" - {file}");
            }

            var namespaces = GetUniqueNamespaces(filesToParse);
            var result = GenerateCombinedSource(namespaces, filesToParse);

            Logger.Instance.WriteLine($"Merged source code size: {result.Length}");
            return result;
        }

        public static string MergeFilesAndDeleteSource(string output)
        {
            try
            {
                return MergeFiles(output);
            }
            finally
            {
                ActionExtensions.SafeInvoke(
                    () => Directory.Delete(
                        Directory.GetParent(output)!.FullName,
                        true));
            }
        }

        public static void CopyFilesAndDeleteSource(string input, string output)
        {
            try
            {
                CopyFolder(input, output);
            }
            finally
            {
                ActionExtensions.SafeInvoke(
                    () => Directory.Delete(
                        Directory.GetParent(input)!.FullName,
                        true));
            }
        }

        private static void CopyFolder(string srcFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            var files = Directory.GetFiles(srcFolder);
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }

            var folders = Directory.GetDirectories(srcFolder);
            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                var dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        private static string GenerateCombinedSource(IEnumerable<string> namespaces, IEnumerable<string> files)
        {
            //
            // NOTE:
            // Swagger Codegen CLI 3.0.14 has a bug where the -DapiTests=false and -DmodelTests=false are not respected
            // Because of this we need to exclude the generated unit test files and the NUnit.* namespaces
            //

            var sb = new StringBuilder();
            foreach (var ns in namespaces.Where(c => !c.Contains("NUnit")).OrderBy(s => s))
                sb.AppendLine("using " + ns + ";");
            sb.AppendLine();

            const string openingTag = "using ";
            foreach (var file in files.Where(c => !c.EndsWith("tests.cs", StringComparison.OrdinalIgnoreCase)))
            {
                var sourceLines = File.ReadAllLines(file);
                foreach (var sourceLine in sourceLines)
                {
                    if (string.IsNullOrWhiteSpace(sourceLine))
                        continue;

                    var trimmedLine = sourceLine.Trim().Replace("  ", " ");
                    var isUsingDir = trimmedLine.StartsWith(openingTag) &&
                                     !trimmedLine.Contains(" var ") &&
                                     trimmedLine.EndsWith(";");

                    if (!isUsingDir)
                        sb.AppendLine(sourceLine);
                }

                sb.AppendLine();
            }

            var sourceCode = sb.ToString();
            return sourceCode;
        }

        private static IEnumerable<string> GetSourceFileNames(string path)
        {
            var queue = new Queue<string>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                var files = Directory.GetFiles(Path.GetFullPath(path));
                foreach (var subDir in Directory.GetDirectories(path))
                    queue.Enqueue(subDir);

                foreach (var file in files.Where(Predicate())) yield return file;
            }
        }

        private static Func<string, bool> Predicate()
        {
            return file => file.EndsWith(".cs") &&
                           !file.Contains("AssemblyInfo.cs");
        }


        private static IEnumerable<string> GetUniqueNamespaces(IEnumerable<string> files)
        {
            var names = new List<string>();
            const string openingTag = "using ";
            const int namespaceStartIndex = 6;

            foreach (var file in files)
            {
                IEnumerable<string> sourceLines = File.ReadAllLines(file);

                foreach (var sourceLine in sourceLines)
                {
                    var line = sourceLine.Trim().Replace("  ", " ");
                    if (!line.StartsWith(openingTag) || line.Contains(" var ") || !line.EndsWith(";"))
                        continue;

                    var name = line.Substring(namespaceStartIndex, line.Length - namespaceStartIndex - 1);
                    if (!names.Contains(name))
                        names.Add(name);
                }
            }

            return names;
        }
    }
}