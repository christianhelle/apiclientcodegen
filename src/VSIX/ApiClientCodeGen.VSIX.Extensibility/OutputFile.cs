namespace ApiClientCodeGen.VSIX.Extensibility;

public static class OutputFile
{
    public static string GetOutputFilename(string inputFile)
    {
        var fileInfo = new FileInfo(inputFile);
        var outputFile = 
            Path.Combine(
                fileInfo.Directory!.FullName,
                char.ToUpper(fileInfo.FullName[fileInfo.Directory.FullName.Length + 1]) +
                fileInfo.FullName.Substring(fileInfo.Directory.FullName.Length + 2))
            .Replace(fileInfo.Extension, ".cs");

        return outputFile;
    }
}
