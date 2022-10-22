using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Rapicgen.Windows;
using Microsoft.VisualStudio.Shell;

namespace Rapicgen.Options.Analytics
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public class AnalyticsOptionPage : DialogPage
    {
        public const string Name = "Analytics";

        public AnalyticsOptionPage()
        {
        }

        protected override IWin32Window Window
            => new AnalyticsOptionsPageCustom();
    }
}
