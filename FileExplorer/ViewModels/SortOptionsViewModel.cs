#region Using Statements

using FileExplorer.Enums;
using FileExplorer.ViewModels;

#endregion

namespace FileExplorer
{
    public class SortOptionsViewModel : ViewModelBase
    {
        #region Fields and Constants

        private SortBy _sortBy;

        private SortDirection _sortDirection;

        #endregion

        #region Properties

        public SortBy SortBy
        {
            get { return _sortBy; }

            set
            {
                if (value != null)
                {
                    if (_sortBy != value)
                    {
                        _sortBy = value;
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public SortDirection SortDirection {

            get { return _sortDirection; }
            
            set
            {
                if (value != null)
                {
                    if (_sortDirection != value)
                    {
                        _sortDirection = value;
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        #endregion
    }
}
