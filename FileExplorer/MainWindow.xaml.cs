#region Using Statements

using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FileExplorer.ViewModels;

#endregion

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields and Constants

        private FilesExplorer _filesExplorer;

        #endregion

        #region Constructors and Deconstructors

        public MainWindow()
        {
            InitializeComponent();
            _filesExplorer = new();
            DataContext = _filesExplorer;
            _filesExplorer.PropertyChanged += FilesExplorer_PropertyChanged;
            _filesExplorer.OnOpenFileRequest += FilesExplorer_OnOpenFileRequest;
        }

        #endregion

        #region Private Methods

        private void OpenDirMenuItem_Click(object sender, RoutedEventArgs e) => _filesExplorer.OpenRootFolderCommand.Execute(null);
        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void FilesExplorer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_filesExplorer.Lang))
                CultureResources.ChangeCulture(CultureInfo.CurrentUICulture);
        }

        private void FilesExplorer_OnOpenFileRequest(object sender, FileInfoViewModel viewModel)
        {
            var content = _filesExplorer.GetFileContent(viewModel);
            if (content is string text)
            {
                FileContent.Text = text;
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (DiskTreeView.SelectedItem is FileSystemInfoViewModel selectedItem)
            {
                try
                {
                    if (selectedItem.Model is DirectoryInfo dir)
                        dir.Delete(recursive: true);
                    else if (selectedItem.Model is FileInfo file)
                        file.Delete();

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Błąd: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem selectedDir)
            {
                var dataContext = (DirectoryInfoViewModel)selectedDir.DataContext;

                var dialog = new CreateDialog(dataContext.Model.FullName);
                if (dialog.ShowDialog() == true)
                {
                    _filesExplorer.RefreshRoot();
                }
            }
        }

        private void FileSelected_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem selectedFile)
            {
                var fileInfoModel = (FileInfoViewModel)selectedFile.DataContext;


                try
                {
                    FileContent.Text = File.ReadAllText(fileInfoModel.Model.FullName);
                }
                catch
                {
                    FileContent.Text = "Błąd wczytywania pliku";
                }

                ShowItemProperties(fileInfoModel.Model);
            }

        }

        private void ShowItemProperties(FileSystemInfo info)
        {
            var attributes = info.Attributes;
            string attr = "";
            attr += attributes.HasFlag(FileAttributes.ReadOnly) ? "r" : "-";
            attr += attributes.HasFlag(FileAttributes.Archive) ? "a" : "-";
            attr += attributes.HasFlag(FileAttributes.System) ? "s" : "-";
            attr += attributes.HasFlag(FileAttributes.Hidden) ? "h" : "-";

            var statusBar = FileProperties;
            statusBar.Items.Clear();

            statusBar.Items.Add(attr);
        }

        #endregion
    }

}