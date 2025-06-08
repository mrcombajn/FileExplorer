#region Using Statements

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using FileExplorer.Collections;
using static System.Windows.Forms.Design.AxImporter;

#endregion

namespace FileExplorer.ViewModels
{
    public class DirectoryInfoViewModel : FileSystemInfoViewModel
    {
        #region

        private FileSystemWatcher _fileSystemWatcher;
        private System.Timers.Timer _reloadTimer;

        #endregion

        #region Constructors and Deconstructors

        public DirectoryInfoViewModel()
        {
            Items.CollectionChanged += Items_CollectionChanged;    
        }

        #endregion

        #region Properties

        public DispatchedObservableCollection<FileSystemInfoViewModel> Items { get; private set; } = new DispatchedObservableCollection<FileSystemInfoViewModel>();

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
                    itemViewModel.StatusMessage = dirInfo.Name;
                    Items.Add(itemViewModel);

                    itemViewModel.Open(dirInfo.FullName);
                }

                foreach (var directoryName in Directory.GetFiles(path))
                {
                    var fileInfo = new DirectoryInfo(directoryName);
                    FileInfoViewModel itemViewModel = new();
                    itemViewModel.Model = fileInfo;
                    itemViewModel.StatusMessage = fileInfo.Name;
                    itemViewModel.SetIcon();

                    Items.Add(itemViewModel);
                }

                _fileSystemWatcher = CreateFileSystemWatcher(path);

            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            return result;
        }

        public void Sort(DirectoryInfoViewModel root, SortOptionsViewModel sortOptions)
        {
            StatusMessage = root.Model.Name;

            if (sortOptions.SortDirection == Enums.SortDirection.Ascending)
                root.Items = new(root.Items.OrderBy(x => GetSortKey(x, sortOptions)));
            else
                root.Items = new(root.Items.OrderByDescending(x => GetSortKey(x, sortOptions)));

            foreach (var item in root.Items.OfType<DirectoryInfoViewModel>())
                Sort(item, sortOptions);

            NotifyPropertyChanged(nameof(Items));
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

        private void OnCreate(object sender, FileSystemEventArgs e) => StartDebouncedReload();

        private void OnRename(object sender, FileSystemEventArgs e) => StartDebouncedReload();

        private void OnDelete(object sender, FileSystemEventArgs e) => StartDebouncedReload();

        private void OnChanged(object sender, FileSystemEventArgs e) => StartDebouncedReload();

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show($"Exception: {e.ToString()}");
        }

        private void StartDebouncedReload()
        {
            if (_reloadTimer != null)
            {
                _reloadTimer.Stop();
                _reloadTimer.Dispose();
            }

            _reloadTimer = new System.Timers.Timer(500); // 500ms opóźnienia
            _reloadTimer.Elapsed += (s, e) =>
            {
                _reloadTimer.Stop();
                _reloadTimer.Dispose();
                _reloadTimer = null;

                System.Windows.Application.Current.Dispatcher.Invoke(() => Reload());
            };
            _reloadTimer.Start();
        }

        private void Reload()
        {
            if (Model == null)
                return;

            if (System.Windows.Application.Current.MainWindow.DataContext is FilesExplorer explorer && explorer.Root?.Model?.FullName == this.Model?.FullName)
            {
                explorer.RefreshRoot();
            }
            else
            {
                Items.Clear();
                Open(Model.FullName);
            }
        }

        private object GetSortKey(FileSystemInfoViewModel item, SortOptionsViewModel options)
        {
            switch (options.SortBy)
            {
                case SortBy.Alphabetically: return item.Caption;
                case SortBy.Extension: return (item.Model as FileInfo)?.Extension ?? "";
                case SortBy.Size: return (item.Model as FileInfo)?.Length ?? 0;
                case SortBy.Modification: return item.LastWriteTime;
                default: return item.Caption;
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in args.NewItems.Cast<FileSystemInfoViewModel>())
                    {
                        item.PropertyChanged += Root_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in args.NewItems.Cast<FileSystemInfoViewModel>())
                    {
                        item.PropertyChanged -= Root_PropertyChanged;
                    }
                    break;
            }
        }

        private void Root_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "StatusMessage" && sender is FileSystemInfoViewModel viewModel)
                this.StatusMessage = viewModel.StatusMessage;
        }

        #endregion

    }
}
