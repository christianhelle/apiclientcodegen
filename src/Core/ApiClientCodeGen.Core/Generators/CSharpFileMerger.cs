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
                    () =>
                    {
                        var parent = Directory.GetParent(output);
                        if (parent != null)
                            Directory.Delete(parent.FullName, true);
                    });
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
                    () =>
                    {
                        var parent = Directory.GetParent(input);
                        if (parent != null)
                            Directory.Delete(parent.FullName, true);
                    });
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

        private static string GenerateCombinedSource(
            IEnumerable<string> namespaces,
            IEnumerable<string> files)
        {
            const string usingTag = "using ";
            const string assemblyTag = "[assembly: ";

            //
            // NOTE:
            // Swagger Codegen CLI 3.0.14 has a bug where the -DapiTests=false and -DmodelTests=false are not respected
            // Because of this we need to exclude the generated unit test files and the NUnit.* namespaces
            //
            var sb = new StringBuilder();
            foreach (var ns in namespaces.Where(c => !c.Contains("NUnit")).OrderBy(s => s))
                sb.AppendLine(usingTag + ns + ";");
            sb.AppendLine();

            foreach (var file in files.Where(c => !c.EndsWith("tests.cs", StringComparison.OrdinalIgnoreCase)))
            {
                var sourceLines = ReadAllLinesWithLongPathSupport(file);
                foreach (var sourceLine in sourceLines)
                {
                    if (string.IsNullOrWhiteSpace(sourceLine))
                        continue;

                    var trimmedLine = sourceLine.Trim().Replace("  ", " ");
                    var writeLine = trimmedLine.StartsWith(usingTag) &&
                                     !trimmedLine.Contains(" var ") &&
                                     trimmedLine.EndsWith(";") ||
                                     trimmedLine.StartsWith(assemblyTag);

                    if (!writeLine)
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
                IEnumerable<string> sourceLines = ReadAllLinesWithLongPathSupport(file);

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

        /// <summary>
        /// Reads all lines from a file with support for long paths on Windows.
        /// Uses FileStream to bypass the 260 character path limitation on older Windows versions.
        /// </summary>
        /// <param name="filePath">The file path to read from</param>
        /// <returns>Array of strings containing all lines from the file</returns>
        private static string[] ReadAllLinesWithLongPathSupport(string filePath)
        {
            try
            {
                // Try the standard File.ReadAllLines first (works on .NET Core/.NET 5+ for most cases)
                return File.ReadAllLines(filePath);
            }
            catch (DirectoryNotFoundException) when (filePath.Length > 260)
            {
                // On Windows with long paths, fall back to FileStream approach
                Logger.Instance.WriteLine($"Using long path support for file: {filePath} (length: {filePath.Length})");
                return ReadFileUsingFileStream(filePath);
            }
            catch (PathTooLongException)
            {
                // Handle PathTooLongException which might occur on older .NET Framework versions
                Logger.Instance.WriteLine($"Using long path support for file: {filePath} (length: {filePath.Length})");
                return ReadFileUsingFileStream(filePath);
            }
        }

        /// <summary>
        /// Reads a file using FileStream to handle long paths
        /// </summary>
        /// <param name="filePath">The file path to read from</param>
        /// <returns>Array of strings containing all lines from the file</returns>
        private static string[] ReadFileUsingFileStream(string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var streamReader = new StreamReader(fileStream);
            
            var lines = new List<string>();
            string? line;
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            
            return lines.ToArray();
        }

        private static IEnumerable<string> GetAssemblyAttributes(IEnumerable<string> files)
        {
            var names = new List<string>();
            const string openingTag = "[assembly: ";
            const int assemblyStartIndex = 11;

            foreach (var file in files)
            {
                IEnumerable<string> sourceLines = ReadAllLinesWithLongPathSupport(file);

                foreach (var sourceLine in sourceLines)
                {
                    var line = sourceLine.Trim().Replace("  ", " ");
                    if (!line.StartsWith(openingTag))
                        continue;

                    var name = line.Substring(assemblyStartIndex, line.Length - assemblyStartIndex - 1);
                    if (!names.Contains(name))
                        names.Add(name);
                }
            }

            return names;
        }
    }
}
