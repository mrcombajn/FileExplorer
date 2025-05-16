
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FileExplorer.ViewModels;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilesExplorer _filesExplorer;

        public MainWindow()
        {
            InitializeComponent();
            _filesExplorer = new();
            DataContext = _filesExplorer;
            _filesExplorer.PropertyChanged += FilesExplorer_PropertyChanged;
        }

        private void OpenDirMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = dlg.SelectedPath;
                _filesExplorer.OpenRoot(path);
            }
        }
        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void FilesExplorer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_filesExplorer.Lang))
                CultureResources.ChangeCulture(CultureInfo.CurrentUICulture);
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


/*               private void ShowItemProperties(object sender, RoutedEventArgs e)
               {
                   var statusBar = FileProperties;
                   statusBar.Items.Clear();

                   var x = (TreeViewItem)e.Source;
                   var properties = FileHandler.GetFileAttributes(x.Tag.ToString());

                   statusBar.Items.Add(new StatusBarItem() { Content = properties });
               }*/

    }

}