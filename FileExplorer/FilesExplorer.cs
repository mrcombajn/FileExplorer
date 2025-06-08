#region Using Statements

using System.ComponentModel;
using System.Globalization;
using System.IO;
using FileExplorer.Database;
using FileExplorer.Dialogs;
using FileExplorer.ViewModels;
using GalaSoft.MvvmLight.Command;

#endregion

namespace FileExplorer
{
    public class FilesExplorer : ViewModelBase
    {
        #region Fields and Statements

        private string _status;

        #endregion

        #region Constructors and Deconstructors 

        public FilesExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));
            OpenRootFolderCommand = new RelayCommand(OpenRootFolderExecuteAsync);
            SortRootFolderCommand = new RelayCommand(
                SortRootFolderExecute,
                CanSortFolderExecute);

            var database = new FileExplorerContext();
        }

        #endregion

        #region Properties

        public DirectoryInfoViewModel? Root { get; set; }

        public string Lang
        {
            get { return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName; }
            set
            {
                if (value != null)
                {
                    if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != value)
                    {
                        CultureInfo.CurrentUICulture = new CultureInfo(value);
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public RelayCommand OpenRootFolderCommand { get; private set; }
        public RelayCommand SortRootFolderCommand { get; private set; }

        public string StatusMessage
        {
            get { return _status; }
            set
            {
                if (value != null)
                {
                    _status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Public Methods

        public void OpenRoot(string path)
        {
            Root = new DirectoryInfoViewModel();
            Root.Model = new DirectoryInfo(path);

            Root.Open(path);
            Root.PropertyChanged += Root_PropertyChanged;
            NotifyPropertyChanged(nameof(Root));
        }

        public void RefreshRoot() => NotifyPropertyChanged(nameof(Root));

        #endregion

        #region Private Methods

        private async void OpenRootFolderExecuteAsync()
        {
            var dlg = new FolderBrowserDialog() { Description = Strings.SelectDirectory };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                await Task.Factory.StartNew(() =>
                {
                    var path = dlg.SelectedPath;
                    OpenRoot(path);
                });
            }
            NotifyPropertyChanged(nameof(Root));
            StatusMessage = Strings.Message_Ready;
        }

        private bool CanSortFolderExecute() => Root != null && Root?.Items?.Count > 0;

        private async void SortRootFolderExecute()
        {
            var dlg = new SortDialog();

            if (dlg.ShowDialog() == true)
            {
                await Task.Factory.StartNew(() =>
                {
                    Root?.Sort(Root, dlg.SortDialogViewModel.SortOptionsViewModel);
                });
            }
        }

        private void Root_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "StatusMessage" && sender is FileSystemInfoViewModel viewModel)
            {
                this.StatusMessage = viewModel.StatusMessage;
            }

        }

        #endregion
    }
}
