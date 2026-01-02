using System.Collections.ObjectModel;

namespace HyRethink.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _statusMessage = string.Empty;

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public MainViewModel()
        {
            StatusMessage = "欢迎使用 HyRethink";
        }
    }
}