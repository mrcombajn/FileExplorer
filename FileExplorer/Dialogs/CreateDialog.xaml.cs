using System;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Win32.SafeHandles;

namespace FileExplorer
{
    /// <summary>
    /// Logika interakcji dla klasy CreateDialog.xaml
    /// </summary>
    public partial class CreateDialog : Window
    {
        private string sourcePath;

        public CreateDialog(string sourcePath)
        {
            InitializeComponent();
            this.sourcePath = sourcePath;
        }

        public void SubmitForm(object sender, RoutedEventArgs e)
        {
            if (FileRadio.IsChecked == true)
                CreateFile();
            else
                CreateDirectory();

            Close();
        }

        public void CancelOperation(object sender, RoutedEventArgs e) => System.Windows.Forms.Application.Exit();

        public void CreateDirectory()
        {
            try
            {
                var textBox = CreateEntityName;

                Directory.CreateDirectory(System.IO.Path.Combine(sourcePath, textBox.Text));
                SetDirectoryAttributes(sourcePath, textBox.Text, GetAttributes());
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        public void CreateFile()
        {
            try
            {
                var textBox = CreateEntityName;

                using(var textReader = File.Create(System.IO.Path.Combine(sourcePath, textBox.Text)));
                SetFileAttributes(sourcePath, textBox.Text, GetAttributes());
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        private void SetDirectoryAttributes(string path, string fileName, List<FileAttributes> attributes)
        {
            if (attributes.Count == 0)
                return;

            SafeFileHandle safeFileHandle = File.OpenHandle(System.IO.Path.Combine(path, fileName), access: FileAccess.Write);

            foreach (var attribute in attributes)
            {
                File.SetAttributes(safeFileHandle, attribute);
            }
        }

        public static void SetFileAttributes(string path, string name, List<FileAttributes> attributes)
        {
            if (attributes.Count == 0)
                return;

            SafeFileHandle safeFileHandle = File.OpenHandle(System.IO.Path.Combine(path, name), access: FileAccess.Write);

            foreach (var attribute in attributes)
            {
                File.SetAttributes(safeFileHandle, attribute);
            }
        }

        public List<FileAttributes> GetAttributes()
        {
            List<FileAttributes> attributes = new();

            if (ReadOnlyOption.IsChecked == true)
                attributes.Add(FileAttributes.ReadOnly);
            if (ArchiveOption.IsChecked == true)
                attributes.Add(FileAttributes.Archive);
            if (HiddenOption.IsChecked == true)
                attributes.Add(FileAttributes.Hidden);
            if (SystemOption.IsChecked == true)
                attributes.Add(FileAttributes.System);

            return attributes;
        }
    }
}
