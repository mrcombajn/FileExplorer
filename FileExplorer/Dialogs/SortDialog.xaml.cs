#region Using Statements

using System.Windows;
using FileExplorer.ViewModels;

#endregion

namespace FileExplorer.Dialogs
{
    /// <summary>
    /// Logika interakcji dla klasy SortDialog.xaml
    /// </summary>
    public partial class SortDialog : Window
    {
        #region Fields and Constants

        private SortDialogViewModel _sortDialogViewModel;

        #endregion

        #region Constructors and Deconstructors


        public SortDialog()
        {
            InitializeComponent();
            _sortDialogViewModel = new();

            DataContext = _sortDialogViewModel;

            Loaded += (s, e) =>
            {
                if (DataContext is SortDialogViewModel vm)
                {
                    vm.RequestClose += (sender, args) =>
                    {
                        DialogResult = true;
                        Close();
                    };
                }
            };
        }

        #endregion

        #region Properties

        public SortDialogViewModel SortDialogViewModel
        {
            get
            {
                return _sortDialogViewModel;
            }
        }

        #endregion
    }
}
