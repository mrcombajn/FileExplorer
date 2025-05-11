#region Using Statements

using System.Collections.ObjectModel;
using System.IO;

#endregion

namespace FileExplorer.ViewModels
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


                    itemViewModel.Open(dirInfo.FullName);
                }

                foreach (var directoryName in Directory.GetFiles(path))
                {
                    var fileInfo = new DirectoryInfo(directoryName);
                    FileInfoViewModel itemViewModel = new();
                    itemViewModel.Model = fileInfo;
                    itemViewModel.SetIcon();

                    Items.Add(itemViewModel);
                }

                CreateFileSystemWatcher(path);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return result;
        }

        #endregion

        #region Private Methods

        private FileSystemWatcher CreateFileSystemWatcher(string path)
        {
            FileSystemWatcher watcher = new(path);

            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

            watcher.Created += OnCreate;
            watcher.Renamed += OnRename;
            watcher.Deleted += OnDelete;
            watcher.Changed += OnChanged;
            watcher.Error += Watcher_Error;
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        private void OnCreate(object sender, FileSystemEventArgs e)
        {
            var papiez = new string("");
        }

        private void OnRename(object sender, FileSystemEventArgs e)
        {
        }

        private void OnDelete(object sender, FileSystemEventArgs e)
        {
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show($"Exception: {e.ToString()}");
        }

        #endregion


    }
}
