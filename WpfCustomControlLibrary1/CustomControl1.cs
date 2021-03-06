using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common.Commands;

namespace WPF.Common
{
    public enum FileType
    {
        Directory,
        File,
    }

    public class PathControl : Control
    {
        static PathControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathControl), new FrameworkPropertyMetadata(typeof(PathControl)));
        }

        public PathControl()
        {
            var template = new ResourceDictionary
            {
                Source = new Uri("DefaultTemplates.xaml", UriKind.Relative)
            };

            Resources.MergedDictionaries.Add(template);
            OpenFileExplorerCommand = new RelayCommand(OpenFileExplorer);
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register
        (
            "Path",
            typeof(string),
            typeof(PathControl),
            new FrameworkPropertyMetadata
            {
                DefaultValue = true,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            }
        );

        public static readonly DependencyProperty FileTypeProperty = DependencyProperty.Register
        (
            nameof(FileType),
            typeof(FileType),
            typeof(PathControl),
            new FrameworkPropertyMetadata
            {
                DefaultValue = FileType.Directory,
                BindsTwoWayByDefault = false,
            }
        );

        public string Path
        {
            get => GetValue(PathProperty)?.ToString() ?? "";
            set => SetValue(PathProperty, value);
        }

        public FileType FileType
        {
            get => (FileType)GetValue(FileTypeProperty);
            set => SetValue(FileTypeProperty, value);
        }

        public RelayCommand OpenFileExplorerCommand { get; }

        public void OpenFileExplorer()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = FileType == FileType.Directory;
            if (!string.IsNullOrWhiteSpace(Path))
                dlg.DefaultFileName = Path;

            switch (dlg.ShowDialog())
            {
                case CommonFileDialogResult.None:
                    Path = "";
                    break;
                case CommonFileDialogResult.Ok:
                    Path = dlg.FileName;
                    break;
                case CommonFileDialogResult.Cancel:
                    break;
            }
        }

    }
}
