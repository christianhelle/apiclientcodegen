using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Views
{
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

        public string OpenApiSpecification { get; private set; }

        public SupportedCodeGenerator SelectedCodeGenerator
            => cbCustomTool.SelectedIndex == -1
                ? default(SupportedCodeGenerator)
                : (SupportedCodeGenerator)cbCustomTool.SelectedIndex;

        public string OutputFilename => $"{tbFilename.Text}.json";

        public EnterOpenApiSpecDialogResult Result { get; private set; }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private void EnterOpenApiSpecDialog_Load(object sender, EventArgs e)
            => SendMessage(
                tbUrl.Handle,
                0x1501, // EM_SETCUEBANNER
                0,
                "Enter OpenAPI Specification URL");

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUrl.Text))
            {
                lblStatus.Text = @"Please enter the URL";
                return;
            }

            if (string.IsNullOrWhiteSpace(tbFilename.Text))
                tbFilename.Text = "Swagger";

            try
            {
                lblStatus.Text = "Downloading...";

                var uri = new Uri(tbUrl.Text);
                using (var client = new WebClient())
                    OpenApiSpecification = await client.DownloadStringTaskAsync(uri);

                if (string.IsNullOrWhiteSpace(OpenApiSpecification))
                {
                    lblStatus.Text = "No content!";
                    Trace.WriteLine($"Unable to download OpenAPI specification file from {tbUrl.Text}");
                    return;
                }

                Trace.WriteLine("OpenAPI Specifications:");
                Trace.WriteLine(OpenApiSpecification);

                Result = new EnterOpenApiSpecDialogResult(
                    OpenApiSpecification,
                    SelectedCodeGenerator,
                    tbFilename.Text + ".json");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UriFormatException ex)
            {
                var message = "Invalid URL";
                lblStatus.Text = message;
                Trace.WriteLine(message);
                Trace.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Unable to download OpenAPI specification file from {tbUrl.Text}");
                Trace.WriteLine(ex);
            }
        }
    }

    public class EnterOpenApiSpecDialogResult
    {
        public EnterOpenApiSpecDialogResult(
            string openApiSpecification,
            SupportedCodeGenerator selectedCodeGenerator,
            string outputFilename)
        {
            OpenApiSpecification = openApiSpecification;
            SelectedCodeGenerator = selectedCodeGenerator;
            OutputFilename = outputFilename;
        }

        public string OpenApiSpecification { get; }
        public SupportedCodeGenerator SelectedCodeGenerator { get; }
        public string OutputFilename { get; }
    }
}
