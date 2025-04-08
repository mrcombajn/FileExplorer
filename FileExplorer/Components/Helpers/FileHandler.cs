using System.IO;
using Microsoft.Win32.SafeHandles;
using static System.Net.Mime.MediaTypeNames;

namespace FileExplorer.Components.Helpers
{
    public class FileHandler
    {
        public static string ReadFile(string filePath)
        {
            string text;

            using (var textReader = File.OpenText(filePath))
            {
                text = textReader.ReadToEnd();
            }
            return text;
        }

        public static void CreateFile(string path, string name) {
            using (var textReader = File.Create(Path.Combine(path, name)));
        } 
        public static void DeleteFile(string filePath) => File.Delete(filePath);
        public static void SetFileAttributes(string path, string name, List<FileAttributes> attributes)
        {
            if (attributes.Count == 0)
                return;

            SafeFileHandle safeFileHandle = File.OpenHandle(Path.Combine(path, name), access: FileAccess.Write);

            foreach (var attribute in attributes)
            {
                File.SetAttributes(safeFileHandle, attribute);
            }
        }
        public static void CreateDirectory(string path, string name) => Directory.CreateDirectory(Path.Combine(path, name));
        public static void DeleteDirectory(string directoryPath) => Directory.Delete(directoryPath, true);

        public static void SetDirectoryAttributes(string path, string name, List<FileAttributes> attributes)
        {
            if (attributes.Count == 0)
                return;

            SafeFileHandle safeFileHandle = File.OpenHandle(Path.Combine(path, name), access: FileAccess.Write);

            foreach (var attribute in attributes)
            {
                File.SetAttributes(safeFileHandle, attribute);
            }
        }

        public static FileAttributes GetFileAttributes(string fullPath)
        {
            return File.GetAttributes(fullPath);
        }

    }
}
