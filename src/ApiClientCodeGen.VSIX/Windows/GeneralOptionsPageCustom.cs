using System;
using System.IO;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    public partial class GeneralOptionsPageCustom : UserControl
    {
        private readonly GeneralOptionPage options;

        public GeneralOptionsPageCustom()
        {
            InitializeComponent();
        }

        public GeneralOptionsPageCustom(
            GeneralOptionPage options)
            : this()
        {
            this.options = options;
            tbJavaPath.Text = options.JavaPath;
            tbNpmPath.Text = options.NpmPath;
            tbNSwagPath.Text = options.NSwagPath;
        }

        private void OpenFileDialog(Control output)
        {
            using (var dialog = new OpenFileDialog())
            {
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

                if (File.Exists(dialog.FileName))
                    output.Text = dialog.FileName;
            }
        }

        private void TbJavaPath_TextChanged(object sender, EventArgs e)
            => options.JavaPath = tbJavaPath.Text;

        private void TbNpmPath_TextChanged(object sender, EventArgs e)
            => options.NpmPath = tbNpmPath.Text;

        private void TbNSwagPath_TextChanged(object sender, EventArgs e)
            => options.NSwagPath = tbNSwagPath.Text;

        private void BtnJavaPath_Click(object sender, EventArgs e)
            => OpenFileDialog(tbJavaPath);

        private void BtnNpmPath_Click(object sender, EventArgs e)
            => OpenFileDialog(tbNpmPath);

        private void BtnNSwagPath_Click(object sender, EventArgs e)
            => OpenFileDialog(tbNSwagPath);
    }
}
