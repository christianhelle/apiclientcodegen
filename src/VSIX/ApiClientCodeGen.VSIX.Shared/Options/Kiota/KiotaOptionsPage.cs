using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Options.Kiota;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Rapicgen.Core.Generators.Kiota;

namespace Rapicgen.Options.Kiota
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public class KiotaOptionsPage : DialogPage, IKiotaOptions
    {
        public const string Name = "Kiota";

        [Category(Name)]
        [DisplayName("Generate Multiple Files")]
        [Description("Generate multiple files for each operation. This only works for SDK style projects")]
        public bool GenerateMultipleFiles { get; set; }

        [Category(Name)]
        [DisplayName("Type Access Modifier")]
        [Description("The access modifier for the generated types")]
        public TypeAccessModifier TypeAccessModifier { get; set; }

        [Category(Name)]
        [DisplayName("Generate Backing Store")]
        [Description("Generate EF backing store code")]
        public bool UsesBackingStore {get; set; }
    }
}
