
namespace FileExplorer.Components
{
    public class FilesExplorer : ViewModelBase
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

        #region Static Methods

        internal static void Created()
        {
            throw new NotImplementedException();
        }

        internal static void Renamed()
        {
            throw new NotImplementedException();
        }

        internal static void Changed()
        {
            throw new NotImplementedException();
        }

        internal static void Deleted()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
