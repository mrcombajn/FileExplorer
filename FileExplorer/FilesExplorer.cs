using System.Globalization;
using FileExplorer.ViewModels;

namespace FileExplorer
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

    }
}
