namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public interface IProgressReporter
    {
        void Progress(uint progress, uint total = 100);
    }
}
