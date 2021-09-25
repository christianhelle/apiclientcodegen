using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class AnalyticsOptionsPageCustom : UserControl
    {
        public AnalyticsOptionsPageCustom()
        {
            InitializeComponent();

            lblSupportKey.Text = $"Support Key: {SupportInformation.GetSupportKey()}";
        }

        private void btnSupportKey_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SupportInformation.GetSupportKey());
        }
    }
}