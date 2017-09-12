namespace Greenbacks.Common.Models
{
    public class CategoryModel : Notifier
    {
        public int Id { get; set; }

        private int _parentCategoryId;
        public int ParentCategoryId
        {
            get { return _parentCategoryId; }
            set
            {
                if (_parentCategoryId == value) { return; }
                _parentCategoryId = value;
                OnPropertyChanged("ParentCategoryId");
            }
        }

        private int _categoryTypeId;
        public int CategoryTypeId
        {
            get { return _categoryTypeId; }
            set
            {
                if (_categoryTypeId == value) { return; }
                _categoryTypeId = value;
                OnPropertyChanged("CategoryTypeId");
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

        private string _subCategoryName;
        public string SubCategoryName
        {
            get { return _subCategoryName; }
            set
            {
                if (string.Equals(_subCategoryName, value)) { return; }
                _subCategoryName = value;
                OnPropertyChanged("SubCategoryName");
            }
        }
    }
}
