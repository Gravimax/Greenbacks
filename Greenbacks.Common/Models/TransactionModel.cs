namespace Greenbacks.Common.Models
{
    public class TransactionModel : Notifier
    {
        public int Id { get; set; }

        private int _accountId;
        public int AccountId
        {
            get { return _accountId; }
            set
            {
                if (_accountId == value) { return; }
                _accountId = value;
                OnPropertyChanged("AccountId");
            }
        }

        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                if (_balance == value) { return; }
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId == value) { return; }
                _categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string _checkNumber;
        public string CheckNumber
        {
            get { return _checkNumber; }
            set
            {
                if (string.Equals(_checkNumber, value)) { return; }
                _checkNumber = value;
                OnPropertyChanged("CheckNumber");
            }
        }

        private bool _clear;
        public bool Clear
        {
            get { return _clear; }
            set
            {
                if (_clear == value) { return; }
                _clear = value;
                OnPropertyChanged("Clear");
            }
        }

        private decimal _credit;
        public decimal Credit
        {
            get { return _credit; }
            set
            {
                if (_credit == value) { return; }
                _credit = value;
                OnPropertyChanged("Credit");
            }
        }

        private decimal _debit;
        public decimal Debit
        {
            get { return _debit; }
            set
            {
                if (_debit == value) { return; }
                _debit = value;
                OnPropertyChanged("Debit");
            }
        }

        private bool _expense;
        public bool Expense
        {
            get { return _expense; }
            set
            {
                if (_expense == value) { return; }
                _expense = value;
                OnPropertyChanged("Expense");
            }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (string.Equals(_memo, value)) { return; }
                _memo = value;
                OnPropertyChanged("Memo");
            }
        }

        private int _payeeId;
        public int PayeeId
        {
            get { return _payeeId; }
            set
            {
                if (_payeeId == value) { return; }
                _payeeId = value;
                OnPropertyChanged("PayeeId");
            }
        }

        private bool _reimbersable;
        public bool Reimbersable
        {
            get { return _reimbersable; }
            set
            {
                if (_reimbersable == value) { return; }
                _reimbersable = value;
                OnPropertyChanged("Reimbersable");
            }
        }

        private bool _taxable;
        public bool Taxable
        {
            get { return _taxable; }
            set
            {
                if (_taxable == value) { return; }
                _taxable = value;
                OnPropertyChanged("Taxable");
            }
        }

        private bool _taxDeductable;
        public bool TaxDeductable
        {
            get { return _taxDeductable; }
            set
            {
                if (_taxDeductable == value) { return; }
                _taxDeductable = value;
                OnPropertyChanged("TaxDeductable");
            }
        }

        private System.DateTime _transactionDate;
        public System.DateTime TransactionDate
        {
            get { return _transactionDate; }
            set
            {
                if (_transactionDate.CompareTo(value) == 0) { return; }
                _transactionDate = value;
                OnPropertyChanged("TransactionDate");
            }
        }
    }
}
