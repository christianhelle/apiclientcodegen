using Microsoft.VisualStudio.Extensibility.UI;
using System.Runtime.Serialization;

namespace ApiClientCodeGen.VSIX.Extensibility.Dialogs;

internal class AddNewInputDialog : RemoteUserControl
{
    public AddNewInputDialog()
        : base(new AddNewInputDialogData())
    {
    }

    public string? Url => ((AddNewInputDialogData)DataContext!).Url;

    [DataContract]
    internal class AddNewInputDialogData
    {
        [DataMember]
        public string? Url { get; set; }
    }
}