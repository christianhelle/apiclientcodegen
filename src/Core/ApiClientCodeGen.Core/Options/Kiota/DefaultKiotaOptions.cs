using Rapicgen.Core.Generators.Kiota;

namespace Rapicgen.Core.Options.Kiota
{
    public class DefaultKiotaOptions : IKiotaOptions
    {
        public bool GenerateMultipleFiles { get; set; } = false;
        public TypeAccessModifier TypeAccessModifier { get; set; } = TypeAccessModifier.Public;
        public bool UseBackingStore { get; set; } = false;
    }
}
