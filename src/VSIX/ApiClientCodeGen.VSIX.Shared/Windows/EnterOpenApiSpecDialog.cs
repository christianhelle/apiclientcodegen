using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Logging;
using Microsoft.VisualStudio.Threading;
using System.Net.Http;

namespace Rapicgen.Windows
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
                    Logger.Instance.WriteLine($"Unable to download OpenAPI specification file from {url}");
                    return;
                }

                Logger.Instance.WriteLine("OpenAPI Specifications:");
                Logger.Instance.WriteLine(openApiSpecification);

                Result = new EnterOpenApiSpecDialogResult(
                    openApiSpecification,
                    tbFilename.Text,
                    url);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UriFormatException ex)
            {
                const string message = "Invalid URL";
                lblStatus.Text = message;
                Logger.Instance.WriteLine(message);
                Logger.Instance.WriteLine(ex);
            }
            catch (HttpRequestException ex)
            {
                lblStatus.Text = ex.Message;
                Logger.Instance.WriteLine(ex.Message);
                Logger.Instance.WriteLine(ex);
            }
            catch (SocketException ex) 
            {
                lblStatus.Text = ex.Message;
                Logger.Instance.WriteLine(ex.Message);
                Logger.Instance.WriteLine(ex);
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                Logger.Instance.TrackError(ex);
                Logger.Instance.WriteLine($"Unable to download OpenAPI specification file from {url}");
                Logger.Instance.WriteLine(ex);
            }
        }

        private async Task<string> DownloadOpenApiSpecAsync()
        {
            var httpMessageHandler = new HttpClientHandler();
            httpMessageHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using var client = new HttpClient(httpMessageHandler);

            foreach (var header in customHeaders)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return await client.GetStringAsync(tbUrl.Text);
        }

        private void btnAddCustomHeaders_Click(object sender, EventArgs e)
        {
            using (var form = new AddCustomHeaderDialog(customHeaders))
            {
                form.ShowDialog();
                customHeaders = form.CustomHeaders;
            }
        }

        private void lblMarketplaceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = string.Empty;
            if (VsPackage.VisualStudioVersion.Major == 17)
                url = "https://bit.ly/rapicgen-vs2022";
            if (VsPackage.VisualStudioVersion.Major == 16)
                url = "https://bit.ly/rapicgen-vs2019";
            if (VsPackage.VisualStudioVersion.Major == 15)
                url = "https://bit.ly/rapicgen-vs2017";

            if (string.IsNullOrWhiteSpace(url))
                return;

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
        }
    }
}