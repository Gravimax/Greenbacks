using System;
using System.IO;

namespace Greenbacks
{
    public static class FileUtilities
    {
        /// <summary>
        /// Selects a file.
        /// </summary>
        /// <param name="Title">The title.</param>
        /// <param name="Filter">The filter.</param>
        /// <param name="FilterIndex">Desired initial index of the filter.</param>
        /// <param name="InitialDir">The initial directory.</param>
        /// <returns>Path to the selected file or null.</returns>
        public static string SelectFile(string Title, string filter, string InitialDir)
        {
            if (string.IsNullOrEmpty(filter)) { filter = "All files|*.*"; }
            if (string.IsNullOrEmpty(InitialDir)) { InitialDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }

            Microsoft.Win32.OpenFileDialog fldg = new Microsoft.Win32.OpenFileDialog();
            fldg.Title = Title;
            fldg.Filter = filter;
            fldg.FilterIndex = 0;
            fldg.InitialDirectory = InitialDir;
            fldg.RestoreDirectory = true;

            if (fldg.ShowDialog() == true)
            {
                return fldg.FileName;
            }

            return null;
        }


        public static string[] LoadFile(string filePath)
        {
            string[] temp = File.ReadAllLines(filePath);
            string[] lines = new string[temp.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                lines[i] = temp[i].Trim();
            }

            return lines;
        }


        public static string GetFileName(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return Path.GetFileName(filePath);
            }
            return string.Empty;
        }


        public static string GetParentDirectory(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                return Directory.GetParent(fileName).FullName;
            }
            return string.Empty;
        }


        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="source">The source path.</param>
        /// <param name="dest">The destination path.</param>
        public static void CopyFile(string source, string dest)
        {
            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(dest))
            {
                if (File.Exists(source)) { File.Copy(source, dest); }
            }
        }


        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static void DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists) { fi.Delete(); }
                fi = null;
            }
        }


        public static bool FileExists(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileInfo fi = new FileInfo(filePath);
                return fi.Exists;
            }
            else
            {
                throw new ArgumentNullException(nameof(filePath), "filePath cannot be empty or null");
            }
        }


        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }


        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }


        public static string GetCurrentDirectory()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
