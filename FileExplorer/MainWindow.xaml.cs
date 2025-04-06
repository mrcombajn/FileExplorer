using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FileExplorer.Components;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryInfo directoryInfo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };

            dlg.ShowDialog();
            directoryInfo = new(dlg.SelectedPath);
            UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            var root = Directories;
            root.Items.Clear();

            UpdateTreeViewRecurive(root, directoryInfo);
        }

        private void UpdateTreeViewRecurive(ItemsControl itemsControl, DirectoryInfo directoryInfo)
        {
            var item = new TreeViewItem
            {
                Header = directoryInfo.Name,
                Tag = directoryInfo.FullName,
            };
            item.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(ShowItemProperties));
            item.ContextMenu = CreateContextMenuForDirs(item.Tag.ToString());

            itemsControl.Items.Add(item);

            var files = directoryInfo.GetFiles();
            var directories = directoryInfo.GetDirectories();

            foreach (var directory in directories)
            {
                UpdateTreeViewRecurive(item, directory);
            }
            UpdateFilesInTreeView(item, files);
        }

        private void UpdateFilesInTreeView(ItemsControl itemsControl, FileInfo[] files)
        {
            foreach (var file in files)
            {
                var item = new TreeViewItem
                {
                    Header = file.Name,
                    Tag = file.FullName
                };
                item.ContextMenu = CreateContextMenuForFiles(item.Tag.ToString());

                item.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler((obiekt, args) => item.ContextMenu.Visibility = Visibility.Visible));
                item.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(ShowItemProperties));

                itemsControl.Items.Add(item);
            }
        }

        private void LoadFileContent(object sender, RoutedEventArgs e)
        {
            var x = (MenuItem)e.Source;

            FileContent.Text = FileHandler.ReadFile(x.Tag.ToString());
        }

        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            var x = (MenuItem)e.Source;
            FileHandler.DeleteFile(x.Tag.ToString());

            Directories.Items.Clear();
            UpdateTreeView();
        } 
        private void DeleteDirectory(object sender, RoutedEventArgs e)
        {
            var x = (MenuItem)e.Source;
            FileHandler.DeleteDirectory(x.Tag.ToString());

            Directories.Items.Clear();
            UpdateTreeView();
        }

        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CreateDialog(object sender, RoutedEventArgs e)
        {
            var x = (MenuItem)e.Source;

            var dialogWindow = new CreateDialog(x.Tag.ToString());

            dialogWindow.ShowDialog();
            Directories.Items.Clear();
            UpdateTreeView();
        }

        private ContextMenu CreateContextMenuForDirs(string itemTag)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem deleteOption = new() { Header = "Delete", Tag = itemTag };
            deleteOption.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(DeleteDirectory));
            MenuItem createOption = new() { Header = "Create", Tag = itemTag };
            createOption.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(CreateDialog));

            menu.Items.Add(deleteOption);
            menu.Items.Add(createOption);

            return menu;
        }

        private ContextMenu CreateContextMenuForFiles(string itemTag)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem openOption = new() { Header = "Open", Tag = itemTag };
            openOption.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(LoadFileContent));
            MenuItem deleteOption = new() { Header = "Delete", Tag = itemTag };
            deleteOption.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(DeleteFile));

            menu.Items.Add(openOption);
            menu.Items.Add(deleteOption);

            return menu;
        }

        private void ShowItemProperties(object sender, RoutedEventArgs e)
        {
            var statusBar = FileProperties;
            statusBar.Items.Clear();

            var x = (TreeViewItem)e.Source;
            var properties = FileHandler.GetFileAttributes(x.Tag.ToString());

            statusBar.Items.Add(new StatusBarItem() { Content = properties });
        }

    }

}