using System.Windows;
using System.Windows.Controls;
using HyRethink.Models;
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

        private bool _isSyncingSelection = false;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isSyncingSelection) return;

            try
            {
                _isSyncingSelection = true;
                
                // 清除 ListView 的选择，保持互斥
                var mediaListView = this.FindName("MediaListView") as ListView;
                if (mediaListView != null && mediaListView.SelectedItems.Count > 0)
                {
                    mediaListView.SelectedItems.Clear();
                }

                UpdateSelectedItems(sender as ListBox);
            }
            finally
            {
                _isSyncingSelection = false;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isSyncingSelection) return;

            try
            {
                _isSyncingSelection = true;

                // 清除 ListBox 的选择，保持互斥
                var mediaListBox = this.FindName("MediaListBox") as ListBox;
                if (mediaListBox != null && mediaListBox.SelectedItems.Count > 0)
                {
                    mediaListBox.SelectedItems.Clear();
                }

                UpdateSelectedItems(sender as ListView);
            }
            finally
            {
                _isSyncingSelection = false;
            }
        }

        private void UpdateSelectedItems(ListBox? listBox)
        {
            if (listBox == null) return;

            MediaViewModel? viewModel = null;
            if (DataContext is MediaViewModel vm)
            {
                viewModel = vm;
            }
            else if (DataContext is MainViewModel mainVm)
            {
                viewModel = mainVm.MediaVM;
            }

            if (viewModel != null)
            {
                viewModel.SelectedMediaItems.Clear();
                foreach (var item in listBox.SelectedItems)
                {
                    if (item is MediaItem mediaItem)
                    {
                        viewModel.SelectedMediaItems.Add(mediaItem);
                    }
                }
                
                // 同时更新 SelectedMediaItem 为最后一个选中的项，以便详情页显示
                if (viewModel.SelectedMediaItems.Count > 0)
                {
                    viewModel.SelectedMediaItem = viewModel.SelectedMediaItems[viewModel.SelectedMediaItems.Count - 1];
                }
                else
                {
                    viewModel.SelectedMediaItem = null;
                }
            }
        }
    }
}
