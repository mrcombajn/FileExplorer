#region Using Statements

using System.Collections.ObjectModel;
using System.IO;
using FileExplorer.Components.FileSystem;
using FileExplorer.Components;

#endregion

namespace FileExplorer
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
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return result;
        }

        #endregion

        #region Private Methods
/*
        private FileSystemWatcher CreateFileSystemWatcher(string path)
        {
            FileSystemWatcher watcher = new(path);
            watcher.Created += OnFileSystemChanged;
            watcher.Renamed += OnFileSystemChanged;
            watcher.Deleted += OnFileSystemChanged;
            watcher.Changed += OnFileSystemChanged;
            watcher.Error += Watcher_Error;
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        protected void OnFileSystemChanged(object sender, EventArgs e)
        {
            var fileSystemWatcher = (FileSystemWatcher)sender;

            e.
            
            switch (fileSystemWatcher.)
            {
                WatcherChangeTypes.Created => FilesExplorer.Created();
                WatcherChangeTypes.Renamed =>  FilesExplorer.Renamed();
                WatcherChangeTypes.Deleted => FilesExplorer.Deleted();
                WatcherChangeTypes.Changed =>  FilesExplorer.Changed();
            }

        }

        protected void Watcher_Error(object sender, EventArgs e)
        {

        }*/

        #endregion
    }
}
