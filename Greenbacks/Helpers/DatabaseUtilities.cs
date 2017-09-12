using System.IO;

namespace Greenbacks
{
    public static class DatabaseUtilities
    {
        #region Create/Backup database


        /// <summary>
        /// Backs up the database.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="backupFile">The backup file name.</param>
        public static void BackupDatabase(string databasePath, string backupFile)
        {
            FileInfo fi = new FileInfo(databasePath);

            if (fi.Exists) // Check if it exists first. If not, no big deal.
            {
                fi.CopyTo(backupFile, true);
            }
        }


        /// <summary>
        /// Restores the database.
        /// </summary>
        /// <param name="backupFile">The backup file.</param>
        /// <param name="destFile">The destination file.</param>
        public static void RestoreDatabaseBackup(string backupFile, string destFile)
        {
            FileInfo fi = new FileInfo(backupFile);

            if (fi.Exists)
            {
                fi.CopyTo(destFile, true);
            }
        }


        /// <summary>
        /// Creates a backup file.
        /// </summary>
        /// <param name="backupFolder">The backup folder.</param>
        /// <param name="backupFileName">Name of the backup file.</param>
        /// <returns>
        /// File path to the backup file.
        /// </returns>
        public static string CreateBackupFile(string backupFolder, string backupFileName, string password)
        {
            string backup = backupFolder + backupFileName + ".bak";
            //using (ZipFile zip = new ZipFile())
            //{
            //    if (!string.IsNullOrEmpty(password))
            //    {
            //        zip.Password = password;
            //    }

            //    zip.Save(backup);
            //}

            return backup;
        }


        #endregion
    }
}
