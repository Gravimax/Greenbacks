namespace Greenbacks.Common.Models
{
    public class PayeeTrackModel : Notifier
    {
        public int Id { get; set; }

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

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value) { return; }
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private short _dueOn;
        public short DueOn
        {
            get { return _dueOn; }
            set
            {
                if (_dueOn == value) { return; }
                _dueOn = value;
                OnPropertyChanged("DueOn");
            }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                if (string.Equals(_note, value)) { return; }
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        public string BankName { get; set; }

        public string PayeeName { get; set; }

        public string DueDate { get; set; }
    }
}
