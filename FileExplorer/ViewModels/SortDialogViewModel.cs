using System.DirectoryServices;
using GalaSoft.MvvmLight.Command;

namespace FileExplorer.ViewModels
{
    public class SortDialogViewModel
    {
        public SortOptionsViewModel SortOptionsViewModel { get; set; } = new SortOptionsViewModel();

        public RelayCommand ConfirmSortCommand { get; }

        public event EventHandler RequestClose;

        public SortDialogViewModel()
        {
            ConfirmSortCommand = new RelayCommand(OnConfirm);
        }

        private void OnConfirm()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
