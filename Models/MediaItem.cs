using System;

namespace HyRethink.Models
{
    /// <summary>
    /// 媒体项目模型，表示一个媒体文件
    /// </summary>
    public class MediaItem
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 文件格式 (如 MP4, MKV)
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小 (格式化后的字符串，如 10 MB)
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// 持续时间 (格式化后的字符串，如 00:05:30)
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// 分辨率 (如 1920x1080)
        /// </summary>
        public string Resolution { get; set; } = string.Empty;

        /// <summary>
        /// 完整文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;
    }
}
