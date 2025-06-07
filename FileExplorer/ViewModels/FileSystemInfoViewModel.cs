#region Using Statements

using System.IO;

#endregion

namespace FileExplorer.ViewModels
{
    public class FileSystemInfoViewModel : ViewModelBase
    {
        #region Fields and Constants

        private DateTime? _lastWriteTime;
        private DateTime? _lastAccessTime;
        private DateTime? _creationTime;

        private string? _caption;
        private string? _extension;
        private int? _data;
        private FileSystemInfo? _fileSystemInfo;

        #endregion

        #region Properties

        public FileSystemInfo? Model
        {
            get { return _fileSystemInfo; }
            set
            {
                if (_fileSystemInfo != value)
                {
                    _fileSystemInfo = value;

                    LastWriteTime = value?.LastWriteTime;
                    LastAccessTime = value?.LastAccessTime;
                    CreationTime = value?.CreationTime;
                    Caption = value?.Name;
                    Extension = value?.Extension;


                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? LastWriteTime
        {
            get { return _lastWriteTime; }
            set
            {
                if (_lastWriteTime != value)
                {
                    _lastWriteTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? LastAccessTime
        {
            get { return _lastAccessTime; }
            set
            {
                if (_lastAccessTime != value)
                {
                    _lastAccessTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? CreationTime
        {
            get { return _creationTime; }
            set
            {
                if (_creationTime != value)
                {
                    _creationTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? Caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? Extension
        {
            get { return _extension; }
            set
            {
                if (_extension != value)
                {
                    _extension = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
