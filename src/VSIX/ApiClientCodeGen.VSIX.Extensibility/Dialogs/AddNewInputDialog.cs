using Microsoft.VisualStudio.Extensibility.UI;

namespace ApiClientCodeGen.VSIX.Extensibility.Dialogs;

internal class AddNewInputDialog : RemoteUserControl
{
    private readonly AddNewInputDialogViewModel _viewModel;

    public AddNewInputDialog()
        : base(new AddNewInputDialogViewModel())
    {
        _viewModel = (AddNewInputDialogViewModel)DataContext!;
    }

    public string? Url => _viewModel.Url;
}