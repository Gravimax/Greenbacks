using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Greenbacks.Views
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddEditCategory : Window, INotifyPropertyChanged
    {
        DALCategories dal_categories = new DALCategories();
        DALCategoryTypes catType = new DALCategoryTypes();

        private List<CategoryTypeModel> _categoryTypes;
        public List<CategoryTypeModel> CategoryTypes
        {
            get { return _categoryTypes; }
            set
            {
                _categoryTypes = value;
                OnPropertyChanged("CategoryTypes");
            }
        }

        private List<GenericComboItem> _parentCategories;
        public List<GenericComboItem> ParentCategories
        {
            get { return _parentCategories; }
            set
            {
                _parentCategories = value;
                OnPropertyChanged("ParentCategories");
            }
        }

        private CategoryModel selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return selectedCategory; }
            set 
            { 
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        /// <summary>
        /// New Category.
        /// </summary>
        public AddEditCategory()
        {
            InitializeComponent();
            SelectedCategory = new CategoryModel();
            CategoryTypes = catType.GetCategoryTypes();
            ParentCategories = new List<GenericComboItem>();
            this.DataContext = this;
        }

        /// <summary>
        /// Edit Category.
        /// </summary>
        /// <param name="category">The category.</param>
        public AddEditCategory(CategoryModel category)
        {
            InitializeComponent();
            SelectedCategory = category;
            CategoryTypes = catType.GetCategoryTypes();
            ParentCategories = new List<GenericComboItem>();
            ParentCategories.Add(new GenericComboItem { ID = 0, Value = "No Parent Category" });
            ParentCategories.AddRange(dal_categories.GetParentCategories(category.CategoryTypeId));
            this.DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory.CategoryTypeId == 1)
            {
                MessageBox.Show("Please select a valid category type", "Invalid Category", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void cmbCatType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParentCategories = new List<GenericComboItem>();
            ParentCategories.Add(new GenericComboItem { ID = 0, Value = "No Parent Category" });
            ParentCategories.AddRange(dal_categories.GetParentCategories(((CategoryTypeModel)cmbCatType.SelectedItem).Id));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
