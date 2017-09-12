using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Greenbacks.View
{
    /// <summary>
    /// Interaction logic for AccountDelete.xaml
    /// </summary>
    public partial class AccountDelete : Window, INotifyPropertyChanged
    {
        private AccountModel _result;

        public AccountModel Result
        {
            get { return _result; }
            set 
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        private bool _BackupAccount;

        public bool BackupAccount
        {
            get { return _BackupAccount; }
            set 
            {
                _BackupAccount = value;
                OnPropertyChanged("BackupAccount");
            }
        }

        public AccountDelete(List<AccountModel> accountList)
        {
            InitializeComponent();
            cmbAccountList.ItemsSource = accountList;
            this.DataContext = this;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAccountList.SelectedItem != null)
            {
                MessageBoxResult mResult = MessageBox.Show("Are you sure?", "Delete account", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mResult == MessageBoxResult.Yes)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    btnCancel_Click(null, null);
                }
            }
            else
            {
                btnCancel_Click(sender, e);
            } 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = null;
            this.DialogResult = false;
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
