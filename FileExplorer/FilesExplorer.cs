#region Using Statements

using System.Globalization;
using System.IO;
using FileExplorer.Dialogs;
using FileExplorer.ViewModels;
using GalaSoft.MvvmLight.Command;

#endregion

namespace FileExplorer
{
    public class FilesExplorer : ViewModelBase
    {
        #region Fields and Constants

        public static readonly string[] TextFilesExtensions = new string[] { ".txt", ".ini", ".log" };

        #endregion

        #region Constructors and Deconstructors 

        public FilesExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));
            OpenRootFolderCommand = new RelayCommand(OpenRootFolderExecute);
            SortRootFolderCommand = new RelayCommand(
                SortRootFolderExecute,
                () => Root != null && Root.Items.Count > 0);
            OpenFileCommand = new RelayCommand(
                () => OnOpenFileRequest.Invoke(),
                OpenFileCanExecute);
        }

        #endregion

        #region Events

        public event EventHandler<FileInfoViewModel> OnOpenFileRequest;

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

        public RelayCommand OpenFileCommand { get; private set; }

        #endregion

        #region Public Methods

        public void OpenRoot(string path)
        {
            Root = new DirectoryInfoViewModel();
            Root.Open(path);
            NotifyPropertyChanged(nameof(Root));
        }

        public void RefreshRoot() => NotifyPropertyChanged(nameof(Root));

        public object GetFileContent(FileInfoViewModel viewModel)
        {
            var extension = viewModel.Extension?.ToLower();
            if (TextFilesExtensions.Contains(extension))
            {
                return GetTextFileContent(viewModel);
            }
            return null;
        }

        #endregion

        #region Private Methods

        private void OpenRootFolderExecute()
        {
            var dlg = new FolderBrowserDialog() { Description = Strings.SelectDirectory };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var path = dlg.SelectedPath;
                OpenRoot(path);
            }
            NotifyPropertyChanged(nameof(Root));
        }

        private void SortRootFolderExecute()
        {
            var dlg = new SortDialog();

            if (dlg.ShowDialog() == true)
            {
                Root?.Sort(Root, dlg.SortDialogViewModel.SortOptionsViewModel);
            }
        }

        private bool OpenFileCanExecute(object parameter)
        {
            if(parameter is FileInfoViewModel viewModel)
            {
                var extension = viewModel.Extension?.ToLower();
                return TextFilesExtensions.Contains(extension);
            }

            return false;
        }

        private string GetTextFileContent(FileInfoViewModel fileInfoViewModel)
        {
            return File.ReadAllText(fileInfoViewModel.Model.FullName);
        }

        #endregion

    }
}
