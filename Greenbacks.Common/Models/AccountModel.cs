namespace Greenbacks.Common.Models
{
    public class AccountModel : Notifier
    {
        public int Id { get; set; }

        private int _accountTypeId;

        public int AccountTypeId
        {
            get { return _accountTypeId; }
            set
            {
                if (_accountTypeId == value) { return; }
                _accountTypeId = value;
                OnPropertyChanged("AccountTypeId");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.Equals(_name, value)) { return; }
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                if (string.Equals(_bankName, value)) { return; }
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
                if (string.Equals(_notes, value)) { return; }
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }


        public string AccountNumber { get; set; }


        public decimal Income { get; set; }

        public decimal Expenses { get; set; }

        public decimal Balance { get; set; }
        public decimal FutureBalance { get; set; }
        public bool Asset { get; set; }

        public string Description
        {
            get { return Name + " - " + string.Format("{0:c}", Balance) + "  (" + string.Format("{0:c}", FutureBalance) + ")"; }
        }

        public string ShortDescription
        {
            get { return "(" + BankName + ") " + Name; }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
