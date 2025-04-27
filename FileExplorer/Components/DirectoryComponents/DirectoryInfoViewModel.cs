#region Using Statements

using System.Collections.ObjectModel;
using System.IO;
using FileExplorer.Components.FileSystem;

#endregion

namespace FileExplorer.Components.DirectoryComponents
{
    public class DirectoryInfoViewModel : FileSystemInfoViewModel
    {
        #region Properties

        public ObservableCollection<FileSystemInfoViewModel> Items { get; private set; } = new ObservableCollection<FileSystemInfoViewModel>();

        public Exception? Exception { get; private set; }

        #endregion

        #region Public Methods

        public bool Open(string path)
        {
            bool result = true;

            try
            {
                foreach (var directoryName in Directory.GetDirectories(path))
                {
                    var dirInfo = new DirectoryInfo(directoryName);
                    DirectoryInfoViewModel itemViewModel = new();
                    itemViewModel.Model = dirInfo;
                    Items.Add(itemViewModel);
                }

                foreach (var directoryName in Directory.GetFiles(path))
                {
                    var fileInfo = new DirectoryInfo(directoryName);
                    DirectoryInfoViewModel itemViewModel = new();
                    itemViewModel.Model = fileInfo;
                    Items.Add(itemViewModel);
                }
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return result;
        }

        #endregion
    }
}
