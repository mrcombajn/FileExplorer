using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FileExplorer.Components.Helpers;

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

                FileHandler.CreateDirectory(sourcePath, textBox.Text);
                FileHandler.SetDirectoryAttributes(sourcePath, textBox.Text, GetAttributes());
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

                FileHandler.CreateFile(sourcePath, textBox.Text);
                FileHandler.SetFileAttributes(sourcePath, textBox.Text, GetAttributes());
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
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
