using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;

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
    }
}
