#region Using Statements

using System.Windows;

#endregion

namespace FileExplorer.Dialogs
{
    /// <summary>
    /// Logika interakcji dla klasy SortDialog.xaml
    /// </summary>
    public partial class SortDialog : Window
    {
        #region Fields and Constants

        private SortOptionsViewModel _sortOptionsViewModel;

        #endregion

        #region Constructors and Deconstructors

        public SortDialog()
        {
            InitializeComponent();
            _sortOptionsViewModel = new();
        }

        #endregion
    }
}
