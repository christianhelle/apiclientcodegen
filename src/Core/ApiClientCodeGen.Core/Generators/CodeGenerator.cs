namespace Rapicgen.Core.Generators
{
    public interface ICodeGenerator
    {
        string GenerateCode(IProgressReporter? pGenerateProgress);
    }
}