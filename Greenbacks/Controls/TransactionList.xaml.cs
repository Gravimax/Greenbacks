using Greenbacks.Common.Models;
using Greenbacks.ViewModels;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for TransactionList.xaml
    /// </summary>
    public partial class TransactionList : UserControl
    {
        public TransactionList(int accountId)
        {
            InitializeComponent();
            TransactionsViewModel viewModel = new TransactionsViewModel();
            this.DataContext = viewModel;
            viewModel.ScrollToRecord += this.OnScrollToRecord;
            viewModel.GetTransactions(accountId);
        }

        protected void OnScrollToRecord(object sender, ScrollToRecordEventArgs e)
        {
            lstvTransList.ScrollIntoView((TransactionModel)e.Record);
        }
    }
}
