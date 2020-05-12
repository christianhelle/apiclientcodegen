using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class AddCustomHeaderDialog : Form
    {
        public AddCustomHeaderDialog()
        {
            InitializeComponent();
        }

        public string Key => tbKey.Text;
        public string Value => tbValue.Text;

        private void AddCustomHeaderDialog_Load(object sender, System.EventArgs e)
        {
            NativeMethods.SendMessage(
                tbKey.Handle,
                NativeMethods.EM_SETCUEBANNER,
                0,
                "Authorization");

            NativeMethods.SendMessage(
                tbValue.Handle,
                NativeMethods.EM_SETCUEBANNER,
                0,
                "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...");
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbKey.Text) || string.IsNullOrWhiteSpace(tbValue.Text))
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
