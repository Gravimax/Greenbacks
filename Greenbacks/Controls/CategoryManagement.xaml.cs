using Greenbacks.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for ManageCategories.xaml
    /// </summary>
    public partial class CategoryManagement : UserControl
    {
        public CategoryManagement()
        {
            InitializeComponent();

            ManageCategoriesViewModel viewModel = new ManageCategoriesViewModel();
            this.DataContext = viewModel;
        }
    }
}
