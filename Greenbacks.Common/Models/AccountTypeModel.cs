namespace Greenbacks.Common.Models
{
    public class AccountTypeModel : Notifier
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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (string.Equals(_description, value)) { return; }
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private bool _asset;
        public bool Asset
        {
            get { return _asset; }
            set
            {
                if (_asset == value) { return; }
                _asset = value;
                OnPropertyChanged("Asset");
            }
        }
    }
}
