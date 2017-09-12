using Greenbacks.Common.Models;
using Greenbacks.ViewModels;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for ManagePayees.xaml
    /// </summary>
    public partial class PayeeManagement : UserControl
    {
        public PayeeManagement(List<AccountModel> accountSummaries)
        {
            InitializeComponent();
            
            ManagePayeesViewModel viewModel = new ManagePayeesViewModel(accountSummaries);
            this.DataContext = viewModel;
        }
    }
}
