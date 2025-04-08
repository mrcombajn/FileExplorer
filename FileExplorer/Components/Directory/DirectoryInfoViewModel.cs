using FileExplorer.Components.FileSystem;

namespace FileExplorer.Components.Directory
{
    public class DirectoryInfoViewModel : FileSystemInfoViewModel
    {
        #region Fields and Constants

        private DateTime _lastWriteTime;

        #endregion

        #region Properties

        public DateTime LastWriteTime
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

        #endregion
    }
}
