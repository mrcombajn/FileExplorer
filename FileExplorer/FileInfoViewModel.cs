namespace FileExplorer
{
    public class FileInfoViewModel : FileSystemInfoViewModel
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
