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

        [Category("Telemetry")]
        [DisplayName("Opt-out")]
        [Description("Set to true to opt-out of telemetry. Default is false.")]
        public bool TelemetryOptOut { get; set; }

        protected override IWin32Window Window
            => new AnalyticsOptionsPageCustom();
    }
}
