namespace Rapicgen.Core.Logging
{
    public interface IConsoleOutput
    {
        void WriteLine(string value);
        void WriteMarkup(string markup);
        void Write(string value);
    }
}