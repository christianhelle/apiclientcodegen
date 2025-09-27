using Rapicgen.Core.Generators.Kiota;

namespace Rapicgen.Core.Options.Kiota
{
    public interface IKiotaOptions
    {
        bool GenerateMultipleFiles { get; }
        
        TypeAccessModifier TypeAccessModifier { get; }

        bool UsesBackingStore { get; }
    }
}
