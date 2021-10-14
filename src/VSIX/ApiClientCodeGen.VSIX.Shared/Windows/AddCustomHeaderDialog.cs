using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class AddCustomHeaderDialog : Form
    {
        private readonly BindingList<CustomHeader> bindingList 
            = new BindingList<CustomHeader>();

        public AddCustomHeaderDialog(
            IReadOnlyDictionary<string, string> existingHeaders = null)
        {
            InitializeComponent();
            dataGridView.DataSource = bindingList;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (existingHeaders == null) 
                return;

            foreach (var keyValuePair in existingHeaders)
                bindingList.Add(
                    new CustomHeader
                    {
                        Key = keyValuePair.Key,
                        Value = keyValuePair.Value
                    });
        }

        public IReadOnlyDictionary<string, string> CustomHeaders
            => bindingList
                .Where(c => !string.IsNullOrWhiteSpace(c.Key) && !string.IsNullOrWhiteSpace(c.Value))
                .ToDictionary(k => k.Key, v => v.Value);

        private sealed class CustomHeader
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
