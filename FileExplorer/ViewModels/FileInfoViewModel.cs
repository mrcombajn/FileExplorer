#region Using Statements

using GalaSoft.MvvmLight.Command;

#endregion

namespace FileExplorer.ViewModels
{
    public class FileInfoViewModel : FileSystemInfoViewModel
    {
        #region Fields and Constants

        private static Dictionary<string, string> iconsDictionary = new()
        {
            { "txt", "/Images/TextFile.png" },
            { "pdf", "/Images/PdfFile.png" },
            { "cs", "/Images/CsFile.png" },
        };

        private string _imageSource;

        #endregion

        #region Constructors and Deconstructors

        public FileInfoViewModel(ViewModelBase owner)
            : base(owner)
        {
            OpenFileCommand = new RelayCommand(() => OpenFileExecute(Caption), OpenFileCanExecute(Caption));
        }

        #endregion

        #region Properties

        public string ImageSource
        {
            get { return _imageSource; }
        }

        #endregion

        #region Public Methods

        public void SetIcon()
        {
            if (iconsDictionary.TryGetValue(Caption.Split(".").Last(), out string value))
            {
                _imageSource = value;
            }
            else
            {
                _imageSource = "DefaultFile.png";
            }
        }

        public RelayCommand OpenFileCommand { get; set; }

        #endregion

        #region Private Methods

        private bool OpenFileCanExecute(object parameter)
        {
            return OwnerExplorer.OpenFileCommand.CanExecute(parameter);
        }
        private void OpenFileExecute(object parameter)
        {
            OwnerExplorer.OpenFileCommand.Execute(parameter);
        }

        #endregion
    }
}
