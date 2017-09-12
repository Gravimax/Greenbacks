using Greenbacks.Common.Models;
using Greenbacks.Helpers;
using Greenbacks.RecordValidation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Greenbacks.View
{
    /// <summary>
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window, INotifyPropertyChanged
    {
        public AccountModel Account { get; set; }

        private List<AccountTypeModel> _accountTypes;
        public List<AccountTypeModel> AccountTypes
        {
            get { return _accountTypes; }
            set 
            { 
                _accountTypes = value;
                OnPropertyChanged("AccountTypes");
            }
        }

        private string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                OnPropertyChanged("AccountName");
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                OnPropertyChanged("BankName");
            }
        }

        private int _accountTypeId;
        public int AccountTypeId
        {
            get { return _accountTypeId; }
            set
            {
                _accountTypeId = value;
                OnPropertyChanged("AccountTypeId");
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public EditAccount(AccountModel account, List<AccountTypeModel> accountTypes)
        {
            InitializeComponent();
            this.Account = account;

            this.AccountName = account.Name;
            this.BankName = account.BankName;
            this.AccountTypeId = account.AccountTypeId;
            this.Notes = account.Notes;

            this.AccountTypes = accountTypes;
            this.DataContext = this;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Account.Name = this.AccountName;
            this.Account.BankName = this.BankName;
            this.Account.AccountTypeId = this.AccountTypeId;

            AccountValidation vr = new AccountValidation(Account);
            vr.Validate();

            if (vr.HasErrors)
            {
                MessageBox.Show(vr.Message, "Validation Errors", MessageBoxButton.OK);
                return;
            }

            this.Account.Notes = this.Notes;
            if (!string.IsNullOrEmpty(pswdAccountNumber.Password))
            {
                this.Account.AccountNumber = Security.GetMd5Hash(pswdAccountNumber.Password.Trim());
            }


            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
