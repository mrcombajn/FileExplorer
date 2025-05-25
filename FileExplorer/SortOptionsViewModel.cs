#region Using Statements

using FileExplorer.Enums;
using FileExplorer.ViewModels;

#endregion

namespace FileExplorer
{
    class SortOptionsViewModel : ViewModelBase
    {
        #region Fields and Constants

        private SortType _sortType;

        private SortDirection _sortDirection;

        #endregion

        #region Properties

        public SortType SortBy
        {
            get { return _sortType; }

            set
            {
                if (value != null)
                {
                    if (_sortType != value)
                    {
                        _sortType = value;
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
