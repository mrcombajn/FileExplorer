#region Using Statements

using System.Globalization;
using FileExplorer.Dialogs;
using FileExplorer.ViewModels;
using GalaSoft.MvvmLight.Command;

#endregion

namespace FileExplorer
{
    public class FilesExplorer : ViewModelBase
    {

        #region Constructors and Deconstructors 

        public FilesExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));
            OpenRootFolderCommand = new RelayCommand(OpenRootFolderExecute);
            SortRootFolderCommand = new RelayCommand(
                SortRootFolderExecute,
                () => true);
            //Root != null && Root.Items.Count > 0
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

        #endregion

        #region Public Methods

        public void OpenRoot(string path)
        {
            Root = new DirectoryInfoViewModel();
            Root.Open(path);
            NotifyPropertyChanged(nameof(Root));
        }

        public void RefreshRoot() => NotifyPropertyChanged(nameof(Root));

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

        #endregion

    }
}
