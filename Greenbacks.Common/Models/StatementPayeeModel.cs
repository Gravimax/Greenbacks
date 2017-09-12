namespace Greenbacks.Common.Models
{
    public class StatementPayeeModel : Notifier
    {
        public int PayeeId { get; set; }


        private string _statementName;
        public string StatementName
        {
            get { return _statementName; }
            set
            {
                if (string.Equals(_statementName, value)) { return; }
                _statementName = value;
                OnPropertyChanged("StatementName");
            }
        }


        public PayeeModel Payee { get; set; }
    }
}
