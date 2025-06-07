#region Using Statements

#endregion

using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;

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

        public FileInfoViewModel()
            : base()
        {
            OpenFileCommand = new RelayCommand(OpenFile, () => true);
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

        private void OpenFile()
        {

        }

        #endregion
    }
}
