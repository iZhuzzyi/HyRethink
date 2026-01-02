using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using HyRethink.Models;
using Microsoft.Win32;

namespace HyRethink.ViewModels
{
    public class MediaViewModel : ViewModelBase
    {
        private MediaItem? _selectedMediaItem;

        /// <summary>
        /// 媒体项目集合
        /// </summary>
        public ObservableCollection<MediaItem> MediaItems { get; } = new ObservableCollection<MediaItem>();

        /// <summary>
        /// 当前选中的媒体项目
        /// </summary>
        public MediaItem? SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        // 命令
        public ICommand AddFileCommand { get; }
        public ICommand AddFolderCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ClearCommand { get; }

        public MediaViewModel()
        {
            AddFileCommand = new RelayCommand(ExecuteAddFile);
            AddFolderCommand = new RelayCommand(ExecuteAddFolder);
            RemoveCommand = new RelayCommand(ExecuteRemove, CanExecuteRemove);
            ClearCommand = new RelayCommand(ExecuteClear, CanExecuteClear);

            // 添加一些测试数据
            AddDummyData();
        }

        private void AddDummyData()
        {
            MediaItems.Add(new MediaItem
            {
                Name = "测试视频1.mp4",
                Format = "MP4",
                Size = "120 MB",
                Duration = "00:05:30",
                Resolution = "1920x1080",
                FilePath = @"C:\Videos\测试视频1.mp4"
            });
            MediaItems.Add(new MediaItem
            {
                Name = "演示动画.mkv",
                Format = "MKV",
                Size = "450 MB",
                Duration = "00:12:15",
                Resolution = "3840x2160",
                FilePath = @"C:\Videos\演示动画.mkv"
            });
        }

        private void ExecuteAddFile(object? parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "媒体文件|*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.mp3;*.wav|所有文件|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                AddFiles(openFileDialog.FileNames);
            }
        }

        private void ExecuteAddFolder(object? parameter)
        {
            // WPF 原生没有 FolderBrowserDialog，这里暂时使用 OpenFileDialog 模拟或需要引入 WinForms/第三方库
            // 为了保持纯净，这里暂时留空或使用简单的逻辑，实际项目中通常使用 Ookii.Dialogs 或 WinForms 引用
            // 这里仅作为占位符，实际实现可能需要 System.Windows.Forms
            
            // 简单模拟：让用户选择文件夹内的一个文件，然后添加该文件夹下的所有媒体文件
            var openFileDialog = new OpenFileDialog
            {
                Title = "请选择文件夹内的一个文件以添加整个文件夹",
                Filter = "所有文件|*.*",
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string? folderPath = Path.GetDirectoryName(openFileDialog.FileName);
                if (!string.IsNullOrEmpty(folderPath))
                {
                    var files = Directory.GetFiles(folderPath, "*.*")
                        .Where(s => s.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) || 
                                    s.EndsWith(".mkv", StringComparison.OrdinalIgnoreCase) ||
                                    s.EndsWith(".avi", StringComparison.OrdinalIgnoreCase));
                    AddFiles(files.ToArray());
                }
            }
        }

        private void ExecuteRemove(object? parameter)
        {
            if (SelectedMediaItem != null)
            {
                MediaItems.Remove(SelectedMediaItem);
                SelectedMediaItem = null;
            }
        }

        private bool CanExecuteRemove(object? parameter)
        {
            return SelectedMediaItem != null;
        }

        private void ExecuteClear(object? parameter)
        {
            MediaItems.Clear();
            SelectedMediaItem = null;
        }

        private bool CanExecuteClear(object? parameter)
        {
            return MediaItems.Count > 0;
        }

        /// <summary>
        /// 添加文件到集合
        /// </summary>
        /// <param name="filePaths">文件路径数组</param>
        public void AddFiles(string[] filePaths)
        {
            foreach (var path in filePaths)
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    MediaItems.Add(new MediaItem
                    {
                        Name = fileInfo.Name,
                        Format = fileInfo.Extension.TrimStart('.').ToUpper(),
                        Size = FormatSize(fileInfo.Length),
                        Duration = "00:00:00", // 暂时无法获取真实时长
                        Resolution = "0x0",    // 暂时无法获取真实分辨率
                        FilePath = path
                    });
                }
            }
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
