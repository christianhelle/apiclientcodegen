using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Rapicgen.Windows
{
    [ExcludeFromCodeCoverage]
    public partial class AddCustomHeaderDialog : Form
    {
        private readonly BindingList<CustomHeader> bindingList = new();

        public AddCustomHeaderDialog(
            IReadOnlyDictionary<string, string>? existingHeaders = null)
        {
            InitializeComponent();
            dataGridView.DataSource = bindingList;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (existingHeaders == null) 
                return;

            foreach (var keyValuePair in existingHeaders)
                bindingList.Add(
                    new CustomHeader(keyValuePair.Key,keyValuePair.Value));
        }

        public IReadOnlyDictionary<string, string> CustomHeaders
            => bindingList
                .Where(c => !string.IsNullOrWhiteSpace(c.Key) && !string.IsNullOrWhiteSpace(c.Value))
                .ToDictionary(k => k.Key, v => v.Value);

        private sealed record CustomHeader(string Key, string Value)
        {
            public string Key { get; } = Key;
            public string Value { get; } = Value;
        }
    }
}
