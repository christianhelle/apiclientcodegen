using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using Microsoft.VisualStudio.Threading;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class EnterOpenApiSpecDialog : Form
    {
        private IReadOnlyDictionary<string, string> customHeaders
            = new Dictionary<string, string>();

        public EnterOpenApiSpecDialog()
        {
            InitializeComponent();
        }

        public static EnterOpenApiSpecDialogResult? GetResult()
        {
            DialogResult dialogResult;
            EnterOpenApiSpecDialogResult? result;
            using (var form = new EnterOpenApiSpecDialog())
            {
                dialogResult = form.ShowDialog();
                result = form.Result;
            }

            return dialogResult == DialogResult.OK ? result : null;
        }

        public EnterOpenApiSpecDialogResult? Result { get; private set; }

        private void EnterOpenApiSpecDialog_Load(object sender, EventArgs e)
            => NativeMethods.SendMessage(
                tbUrl.Handle,
                NativeMethods.EM_SETCUEBANNER,
                0,
                "Enter OpenAPI Specification URL (e.g. https://petstore.swagger.io/v2/swagger.json)");

        private void BtnOK_Click(object sender, EventArgs e)
        {
            OnButtonClickAsync().Forget();
        }

        private async Task OnButtonClickAsync()
        {
            var url = tbUrl.Text;
            if (string.IsNullOrWhiteSpace(url))
            {
                lblStatus.Text = @"Please enter the URL";
                return;
            }

            if (string.IsNullOrWhiteSpace(tbFilename.Text))
                tbFilename.Text = "Swagger";

            try
            {
                lblStatus.Text = "Downloading...";

                var openApiSpecification = await DownloadOpenApiSpecAsync();
                if (string.IsNullOrWhiteSpace(openApiSpecification))
                {
                    lblStatus.Text = "No content!";
                    Trace.WriteLine($"Unable to download OpenAPI specification file from {url}");
                    return;
                }

                Trace.WriteLine("OpenAPI Specifications:");
                Trace.WriteLine(openApiSpecification);

                Result = new EnterOpenApiSpecDialogResult(
                    openApiSpecification,
                    tbFilename.Text,
                    url);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UriFormatException ex)
            {
                Logger.Instance.TrackError(ex);
                const string message = "Invalid URL";
                lblStatus.Text = message;
                Trace.WriteLine(message);
                Trace.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Trace.WriteLine($"Unable to download OpenAPI specification file from {url}");
                Trace.WriteLine(ex);
            }
        }

        private async Task<string> DownloadOpenApiSpecAsync()
        {
            using (var client = new WebClient())
            {
                foreach (var header in customHeaders)
                    client.Headers.Add(header.Key, header.Value);

                return await client.DownloadStringTaskAsync(
                    new Uri(tbUrl.Text));
            }
        }

        private void btnAddCustomHeaders_Click(object sender, EventArgs e)
        {
            using (var form = new AddCustomHeaderDialog(customHeaders))
            {
                form.ShowDialog();
                customHeaders = form.CustomHeaders;
            }
        }
    }
}
