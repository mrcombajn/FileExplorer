#region Using Statements

#endregion

using System.Windows;
using System.Windows.Controls;

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
                _imageSource = "/Images/DefaultFile.png";
            }
        }

        #endregion
    }
}
