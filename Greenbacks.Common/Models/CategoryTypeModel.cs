namespace Greenbacks.Common.Models
{
    public class CategoryTypeModel : Notifier
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
    }
}
