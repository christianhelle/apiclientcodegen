namespace Rapicgen.Core.Options.NSwag
{
    /// <summary>
    /// C# class style for generated code.
    /// </summary>
    public enum CSharpClassStyle
    {
        /// <summary>
        /// Plain Old C# Objects (default)
        /// </summary>
        Poco,

        /// <summary>
        /// Implements INotifyPropertyChanged
        /// </summary>
        Inpc,

        /// <summary>
        /// Prism base class
        /// </summary>
        Prism,

        /// <summary>
        /// C# 9.0 Records
        /// </summary>
        Record
    }
}
