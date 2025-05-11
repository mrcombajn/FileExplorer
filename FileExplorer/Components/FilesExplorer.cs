
using System.Globalization;

namespace FileExplorer.Components
{
    public class FilesExplorer : ViewModelBase
    {
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

        #endregion

        #region Constructors and Deconstructors 

        public FilesExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));
        }

        #endregion

        #region Public Methods

        public void OpenRoot(string path)
        {
            Root = new DirectoryInfoViewModel();
            Root.Open(path);
        }

        #endregion

        #region Static Methods

        internal static void Created()
        {
            throw new NotImplementedException();
        }

        internal static void Renamed()
        {
            throw new NotImplementedException();
        }

        internal static void Changed()
        {
            throw new NotImplementedException();
        }

        internal static void Deleted()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
