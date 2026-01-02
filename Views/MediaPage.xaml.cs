using System.Windows;
using System.Windows.Controls;
using HyRethink.ViewModels;

namespace HyRethink.Views
{
    /// <summary>
    /// 媒体页面 - 媒体管理和浏览功能
    /// </summary>
    public partial class MediaPage : UserControl
    {
        public MediaPage()
        {
            InitializeComponent();
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[]? files = (string[]?)e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    if (DataContext is MediaViewModel viewModel)
                    {
                        viewModel.AddFiles(files);
                    }
                    // 如果 DataContext 是 MainViewModel，则需要访问其 MediaVM 属性
                    else if (DataContext is MainViewModel mainViewModel)
                    {
                        mainViewModel.MediaVM.AddFiles(files);
                    }
                }
            }
        }
    }
}
