namespace Greenbacks.Common.Models
{
    public class PayeeModel : Notifier
    {
        public int Id { get; set; }

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
    }
}
