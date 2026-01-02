using System.Collections.ObjectModel;

namespace HyRethink.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _statusMessage = string.Empty;
        private int _selectedTabIndex = -1;

        // 子页面 ViewModel 实例
        public WelcomeViewModel WelcomeVM { get; } = new WelcomeViewModel();
        public MediaViewModel MediaVM { get; } = new MediaViewModel();
        public OperationsViewModel OperationsVM { get; } = new OperationsViewModel();
        public TasksViewModel TasksVM { get; } = new TasksViewModel();
        public QueueViewModel QueueVM { get; } = new QueueViewModel();

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// 当前选中的 Tab 索引
        /// -1 表示未选中任何 Tab (显示欢迎页面)
        /// </summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        public MainViewModel()
        {
            StatusMessage = "欢迎使用 HyRethink";
        }
    }
}