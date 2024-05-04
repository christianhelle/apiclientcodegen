using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Rapicgen.Core.Logging;
using Rapicgen.Options.Analytics;

namespace Rapicgen.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class AnalyticsOptionsPageCustom : UserControl
    {
        private readonly AnalyticsOptionPage analyticsOptionPage;

        public AnalyticsOptionsPageCustom()
        {
            InitializeComponent();

            lblSupportKey.Text = $"Support Key: {SupportInformation.GetSupportKey()}";
        }

        public AnalyticsOptionsPageCustom(
            AnalyticsOptionPage analyticsOptionPage)
            : this()
        {
            this.analyticsOptionPage = analyticsOptionPage;
            chkDisableTelemetry.Checked = analyticsOptionPage.TelemetryOptOut;
        }

        private void btnSupportKey_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SupportInformation.GetSupportKey());
        }

        private void chkDisableTelemetry_CheckedChanged(object sender, EventArgs e)
        {
            analyticsOptionPage.TelemetryOptOut = chkDisableTelemetry.Checked;
        }
    }
}