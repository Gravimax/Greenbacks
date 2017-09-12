using Greenbacks.Helpers;
using Greenbacks.Models;
using System;
using System.IO;

namespace Greenbacks
{
    internal static class ApplicationConfiguration
    {
        private static string baseFilePath = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "GreenbacksConfig.xml");

        public static GreenbacksConfiguration GreenbacksConfig;

        public static GreenbacksConfiguration Load()
        {
            GreenbacksConfiguration config;

            try
            {
                FileInfo fi = new FileInfo(baseFilePath);
                if (!fi.Exists)
                {
                    config = new GreenbacksConfiguration
                    {
                        RememberLastLocation = false,
                        WindowHeight = 0,
                        WindowWidth = 0,
                        WindowLeft = 0,
                        WindowTop = 0
                    };

                    Save(config);
                }
                else
                {
                    config = XMLFileIO.Load<GreenbacksConfiguration>(baseFilePath);
                }

                return config;
            }
            catch (Exception ex)
            {
                ExceptionLogger.AppendExceptionLog(ex);

                return new GreenbacksConfiguration
                {
                    RememberLastLocation = false,
                    WindowHeight = 0,
                    WindowWidth = 0,
                    WindowLeft = 0,
                    WindowTop = 0
                };
            }
        }

        public static void Save(GreenbacksConfiguration config)
        {
            try
            {
                XMLFileIO.Save<GreenbacksConfiguration>(baseFilePath, config);
            }
            catch (Exception ex)
            {
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }

        private static string backupfilePath;
        private static string autoBackupPath;

        public static void CheckApplicationFolders()
        {
            try
            {
                backupfilePath = baseFilePath + "Backup/";
                autoBackupPath = backupfilePath + "AutoBackup/";

                if (!Directory.Exists(baseFilePath))
                {
                    Directory.CreateDirectory(baseFilePath);
                    Directory.CreateDirectory(autoBackupPath);
                }
                else
                {
                    if (!Directory.Exists(autoBackupPath))
                    {
                        Directory.CreateDirectory(autoBackupPath);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }
    }
}
