using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public static class CSharpFileMerger
    {
        public static string MergeFiles(string folder)
        {
            var filesToParse = GetSourceFileNames(folder).ToList();
            var namespaces = GetUniqueNamespaces(filesToParse);
            return GenerateCombinedSource(namespaces, filesToParse);
        }

        public static string MergeFilesAndDeleteSource(string output)
        {
            try
            {
                return MergeFiles(output);
            }
            finally
            {
                try
                {
                    Directory.Delete(output, true);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
        }

        private static string GenerateCombinedSource(IEnumerable<string> namespaces, IEnumerable<string> files)
        {
            //
            // NOTE:
            // Swagger Codegen CLI 3.0.11 has a bug where the -DapiTests=false and -DmodelTests=false are not respected
            // Because of this we need to exclude the generated unit test files and the NUnit.* namespaces
            //

            var sb = new StringBuilder();
            foreach (var ns in namespaces.Where(c => !c.Contains("NUnit")).OrderBy(s => s))
                sb.AppendLine("using " + ns + ";");

            const string openingTag = "using ";
            foreach (var file in files.Where(c => !c.EndsWith("tests.cs", StringComparison.OrdinalIgnoreCase)))
            {
                var sourceLines = File.ReadAllLines(file);
                foreach (var sourceLine in sourceLines)
                {
                    if (string.IsNullOrWhiteSpace(sourceLine))
                        continue;

                    var trimmedLine = sourceLine.Trim().Replace("  ", " ");
                    var isUsingDir = trimmedLine.StartsWith(openingTag) && trimmedLine.EndsWith(";");

                    if (!isUsingDir)
                        sb.AppendLine(sourceLine);
                }
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
                string[] files = null;
                path = queue.Dequeue();
                try
                {
                    files = Directory.GetFiles(path);
                    foreach (var subDir in Directory.GetDirectories(path)) queue.Enqueue(subDir);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                }

                if (files == null)
                    continue;

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
                    var trimmedLine = sourceLine.Trim().Replace("  ", " ");
                    if (!trimmedLine.StartsWith(openingTag) || !trimmedLine.EndsWith(";"))
                        continue;

                    var name = trimmedLine.Substring(namespaceStartIndex, trimmedLine.Length - namespaceStartIndex - 1);
                    if (!names.Contains(name))
                        names.Add(name);
                }
            }

            return names;
        }
    }
}