using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ApiClientCodeGen.VSIX.Extensibility.Dialogs;

internal class AddNewInputDialogViewModel : INotifyPropertyChanged
{
    private string _url = string.Empty;

    public string Url
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                _url = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
