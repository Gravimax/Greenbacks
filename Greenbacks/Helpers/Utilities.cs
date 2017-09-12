using Greenbacks.Common.Models;
using Greenbacks.Models;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows;

namespace Greenbacks
{
    public class Utilities
    {
        public static void FormatTrackDate(PayeeTrackModel track)
        {
            var now = DateTime.Now;

            if (track.DueOn < now.Day)
            {
                var future = now.AddMonths(1);
                track.DueDate = new DateTime(future.Year, future.Month, track.DueOn).ToShortDateString();
            }
            else
            {
                track.DueDate = new DateTime(now.Year, now.Month, track.DueOn).ToShortDateString();
            }
        }


        public static void ShowMessageBox(string message)
        {
            MessageBox.Show(message, AssemblyInfoHelper.Product, MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public static void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(message, AssemblyInfoHelper.Product, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static void ShowWarningMessageBox(string message)
        {
            MessageBox.Show(message, AssemblyInfoHelper.Product, MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        public static bool ShowDialogBox(string message)
        {
            var result = MessageBox.Show(message, AssemblyInfoHelper.Product, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                return true;
            }

            return false;
        }


        public static string HashData(string unHashed)
        {
            if (!string.IsNullOrEmpty(unHashed))
            {
                //Get the string and convert to a byte array
                byte[] data = System.Text.Encoding.UTF8.GetBytes(unHashed);
                byte[] hash;

                //Hash the byte array
                hash = SHA512.Create().ComputeHash(data);

                //Convert the hashed byte array back to a string
                return Convert.ToBase64String(hash);
            }

            return null;
        }

        public static string SaveFile(string fileName = "Account Export")
        {
            string extension = "xml";

            SaveFileDialog sfd = new SaveFileDialog()
            {
                DefaultExt = extension,
                FileName = fileName + "." + extension,
                Filter = String.Format("{1} (*.{0})|*.{0}", extension, "XML File"),
                FilterIndex = 1
            };

            if (sfd.ShowDialog() == true)
            {
                return sfd.FileName;
            }

            return null;
        }

        public static string OpenFile()
        {
            string extension = "xml";

            OpenFileDialog ofd = new OpenFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} (*.{0})|*.{0}", extension, "XML File"),
                FilterIndex = 1
            };

            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }

            return null;
        }

        public static string GetConnectionString()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            return connectionStringsSection.ConnectionStrings["GreenbacksEntities"].ConnectionString.ToString();
        }

        /// <summary>
        /// Updates the connection string at runtime. Does not save app.config.
        /// </summary>
        /// <param name="attachDbFileName">Name of the attach database file.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        public static void UpdateConnectionString(string attachDbFileName, string userName, string userPassword)
        {
            // Retrieve the connection string named databaseConnection 
            // from the application's app.config or web.config file.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            // Create new conneciton string.
            ConnectionProperties props = new ConnectionProperties();
            props.AttachDbFileName = attachDbFileName;
            props.Username = userName;
            props.Password = userPassword;

            connectionStringsSection.ConnectionStrings["GreenbacksEntities"].ConnectionString = props.ConnectionString;
            connectionStringsSection.ConnectionStrings["GreenbacksEntities"].ProviderName = "System.Data.EntityClient";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");

            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["GreenbacksEntities"];

            //if (null != settings)
            //{
            //    // Retrieve the partial connection string. 
            //    string connectString = settings.ConnectionString;

            //    // Create a new SqlConnectionStringBuilder based on the 
            //    // partial connection string retrieved from the config file.
            //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);

            //    if (!builder.AttachDBFilename.Equals(attachDbFileName))
            //    {
            //        // Supply the additional values.
            //        //builder.Add("metadata", "res://*/Greentacks.csdl|res://*/Greentacks.ssdl|res://*/Greentacks.msl");
            //        builder.DataSource = "(LocalDB)\v11.0";
            //        builder.AttachDBFilename = attachDbFileName;
            //        builder.InitialCatalog = "Greenbacks";
            //        builder.IntegratedSecurity = true;
            //        builder.MultipleActiveResultSets = true;
            //        builder.ApplicationName = "EntityFramework";

            //        if (!string.IsNullOrEmpty(userName))
            //        {
            //            builder.UserID = userName;
            //            builder.Password = userPassword;
            //        }

            //        settings.ConnectionString = builder.ConnectionString;
            //    }
            //}
        }
    }
}
