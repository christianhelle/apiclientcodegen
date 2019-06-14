using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class EnterOpenApiSpecDialog : Form
    {
        public EnterOpenApiSpecDialog()
        {
            InitializeComponent();
        }

        public static EnterOpenApiSpecDialogResult GetResult()
        {
            DialogResult dialogResult;
            EnterOpenApiSpecDialogResult result;
            using (var form = new EnterOpenApiSpecDialog())
            {
                dialogResult = form.ShowDialog();
                result = form.Result;
            }

            return dialogResult == DialogResult.OK ? result : null;
        }

        public EnterOpenApiSpecDialogResult Result { get; private set; }

        private void EnterOpenApiSpecDialog_Load(object sender, EventArgs e)
            => NativeMethods.SendMessage(
                tbUrl.Handle,
                NativeMethods.EM_SETCUEBANNER,
                0,
                "Enter OpenAPI Specification URL (e.g. https://petstore.swagger.io/v2/swagger.json)");

        private async void BtnOK_Click(object sender, EventArgs e)
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
                const string message = "Invalid URL";
                lblStatus.Text = message;
                Trace.WriteLine(message);
                Trace.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Unable to download OpenAPI specification file from {url}");
                Trace.WriteLine(ex);
            }
        }

        private async Task<string> DownloadOpenApiSpecAsync()
        {
            using (var client = new WebClient())
                return await client.DownloadStringTaskAsync(
                    new Uri(tbUrl.Text));
        }
    }
}
