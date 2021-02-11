namespace ApiClientCodeGen.VSMac.Commands
{
    public interface ICodeGeneratorCommand
    {
        void Run(string file, string customNamespace = null);
    }
}