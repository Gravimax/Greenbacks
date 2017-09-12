using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Helpers;
using Greenbacks.Models;
using Greenbacks.Views;
using System;
using System.Collections.Generic;

namespace Greenbacks.ViewModels
{
    public class ManageCategoriesViewModel : ViewModelBase
    {
        private List<GenericComboItem> _categoryList;
        public List<GenericComboItem> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                OnPropertyChanged("CategoryList");
            }
        }

        private GenericComboItem _selectedCategory;
        public GenericComboItem SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public DelegateCommand NewCategoryCommand { get; private set; }
        public DelegateCommand EditCategoryCommand { get; private set; }
        public DelegateCommand DeleteCategoryCommand { get; private set; }
        public DelegateCommand EditDoubleClickItemCommand { get; private set; }

        public ManageCategoriesViewModel()
        {
            DALCategories dal_Categories = new DALCategories();
            CategoryList = dal_Categories.GetCategoriesList();
            NewCategoryCommand = new DelegateCommand(CreateCategory);
            EditCategoryCommand = new DelegateCommand(EditCategory, CanEditCategory);
            DeleteCategoryCommand = new DelegateCommand(DeleteCategory, CanDeleteCategory);
            EditDoubleClickItemCommand = new DelegateCommand(CategoryDoubleClick);
        }

        public void CreateCategory(object args)
        {
            try
            {
                DALCategories dal_Categories = new DALCategories();
                AddEditCategory addEdit = new AddEditCategory();

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    addEdit.Owner = owner;
                }

                addEdit.Closed += (sender, e) =>
                {
                    if (addEdit.DialogResult == true)
                    {
                        try
                        {
                            dal_Categories.CreateCategory(addEdit.SelectedCategory);
                            CategoryList = dal_Categories.GetCategoriesList();
                        }
                        catch (Exception ex)
                        {
                            OnMessageUpdated(ex.Message, true);
                        }
                    }
                };

                addEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }

        private void CategoryDoubleClick(object args)
        {
            if (CanEditCategory(null))
            {
                EditCategory(args);
            }
        }

        private bool CanEditCategory(object args)
        {
            return SelectedCategory != null;
        }

        public void EditCategory(object args)
        {
            try
            {
                DALCategories dal_Categories = new DALCategories();
                CategoryModel category = dal_Categories.GetCategory(SelectedCategory.ID);
                AddEditCategory addEdit = new AddEditCategory(category);

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    addEdit.Owner = owner;
                }

                addEdit.Closed += (sender, e) =>
                {
                    if (addEdit.DialogResult == true)
                    {
                        try
                        {
                            dal_Categories.UpdateCategory(addEdit.SelectedCategory);
                            CategoryList = dal_Categories.GetCategoriesList();
                        }
                        catch (Exception ex)
                        {
                            OnMessageUpdated(ex.Message, true);
                        }
                    }
                };

                addEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }

        private bool CanDeleteCategory(object args)
        {
            return SelectedCategory != null;
        }

        public void DeleteCategory(object args)
        {
            try
            {
                DALCategories dal_Categories = new DALCategories();
                if (!dal_Categories.IsCategoryInUse(SelectedCategory.ID))
                {
                    if (Utilities.ShowDialogBox("Delete Category?") == true)
                    {
                        try
                        {
                            dal_Categories.DeleteCategory(SelectedCategory.ID);
                            OnMessageUpdated("Category Deleted", false);

                        }
                        catch (Exception ex)
                        {
                            OnMessageUpdated(ex.Message, true);
                        }
                    }
                }
                else
                {
                    OnMessageUpdated("Cannot Delete Category. Category is currently in use.", true);
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }
    }
}
