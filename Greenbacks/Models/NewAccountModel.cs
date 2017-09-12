using Greenbacks.Common;

namespace Greenbacks.Models
{
    public class NewAccountModel : Notifier
    {
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

        private string _accountNumber;

        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                _accountNumber = value;
                OnPropertyChanged("AccountNumber");
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

        private bool _asset;
        public bool Asset
        {
            get { return _asset; }
            set
            {
                _asset = value;
                OnPropertyChanged("Asset");
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

        private decimal _startingBalance;
        public decimal StartingBalance
        {
            get { return _startingBalance; }
            set
            {
                _startingBalance = value;
                OnPropertyChanged("StartingBalance");
            }
        }
    }
}
