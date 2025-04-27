using System.Collections.ObjectModel;
using FileExplorer.Components.FileSystem;

namespace FileExplorer.Components.Directory
{
    public class DirectoryInfoViewModel : FileSystemInfoViewModel
    {
        #region Properties

        public ObservableCollection<FileSystemInfoViewModel> Items { get; private set; } = new ObservableCollection<FileSystemInfoViewModel>();

        #endregion
    }
}
