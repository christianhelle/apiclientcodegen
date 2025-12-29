namespace ApiClientCodeGen.VSIX.Extensibility;

public static class OutputFile
{
    public static string GetOutputFilename(string inputFile)
    {
        var fileInfo = new FileInfo(inputFile);
        var fileName = fileInfo.Name;
        var directoryPath = fileInfo.DirectoryName
            ?? Path.GetDirectoryName(inputFile)
            ?? string.Empty;

        // Capitalize the first character safely
        var safeFileName = string.IsNullOrEmpty(fileName) 
            ? string.Empty 
            : char.ToUpper(fileName[0]) + fileName.Substring(1);

        var outputFile = Path.ChangeExtension(
            Path.Combine(directoryPath, safeFileName), 
            ".cs");

        return outputFile;
    }
}
