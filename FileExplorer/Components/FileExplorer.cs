namespace FileExplorer.Components
{
    public class FileExplorer : ViewModelBase
    {
        #region Properties

        public DirectoryInfoViewModel? Root { get; set; }

        #endregion

        #region Public Methods

        public void OpenRoot(string path)
        {
            Root = new DirectoryInfoViewModel();
            Root.Open(path);
        }

        #endregion
    }
}
